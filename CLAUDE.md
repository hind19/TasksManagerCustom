# TasksManagerCustom — Claude Code Context

## Project Overview

A portable, single-user task manager desktop application built with WPF (.NET 6/8).  
**Training/pet project.** Not intended for commercial or private production use.  
Active development: September 2024 – present (see last commit date for actual status).

Goal: learn Prism, WPF modularity, loose-coupled persistence, localization, and custom controls.

---

## Stack

| Layer | Technology |
|-------|-----------|
| UI | WPF, XAML |
| MVVM / DI / Navigation | Prism 8.1 (DryIoc container) |
| Persistence | SQLite via sqlite-net-pcl |
| Object mapping | AutoMapper |
| Extra controls | Extended.Wpf.Toolkit (DateTimePicker) |
| Tests | xUnit + Moq |

---

## Solution Structure

```
TasksManagerCustom.sln
│
├── TasksManagerCustom/TasksManagerCustom/          ← Main WPF application (entry point)
│   ├── App.xaml.cs                                 ← Prism bootstrap, DI registration, localization
│   ├── Views/MainWindow.xaml                       ← Shell with Prism regions
│   ├── Dialogs/CategoriesDialogs/                  ← Add/Update Category dialog
│   ├── Dialogs/TasksDialogs/                       ← Add/Update Task dialog (WIP)
│   ├── Models/                                     ← View-level models (CategoryModel, NameValuePair)
│   ├── AutomapperProfiles/                         ← Category mapping profiles for app layer
│   └── Languages/en-US.xaml, ru-RU.xaml            ← Localization resource dictionaries
│
├── TasksManagerCustom/TasksManagerCustom.Core/     ← Shared kernel (no external deps except Prism)
│   ├── Events/                                     ← Prism PubSubEvents for inter-module messaging
│   ├── EventModels/HierarchicalCollectionModel     ← Tree node model for left panel
│   ├── Mvvm/ViewModelBase.cs                       ← Base VM (IDestructible)
│   └── RegionNames.cs                              ← Prism region name constants
│
├── TasksManagerCustom.Shared/                      ← Cross-cutting constants and enums
│   ├── Enums/TaskStatusEnum.cs
│   ├── GlobalConstants/GlobalConstants.cs          ← Image file name constants
│   └── ErrorMessages.cs
│
├── TasksManager.Services.Interfaces/               ← Service-layer contracts (interfaces + service DTOs)
│   ├── DTOs/  (AddUpdateCategoryDto, CategoryDto, ShortCategoryDto, TaskDto)
│   ├── RepositoryServices/ (ICategoryRepositoryCommandService, ICategoryRepositoryQueryService,
│   │                        ITaskCommandService, ITasksQueryService)
│   └── IMessageService.cs
│
├── TasksManager.Services/                          ← Service implementations
│   ├── RepositoryServices/ (Category/Task Command/Query services)
│   ├── Automapper/ServicesProfile.cs               ← AutoMapper profile for service ↔ persistence DTOs
│   ├── RepositoryFactory.cs                        ← Manual factory (see notes below)
│   ├── DatabaseService.cs                          ← DB initialization facade
│   └── RepositoriesEnum.cs                         ← (currently unused)
│
├── TasksManager.PersistenceContracts/              ← Persistence-layer contracts
│   ├── Dtos/ (PersistenceCategoryDto, PersistenceTaskDto)
│   └── Repositories/ (IRepository, ICategoryRepository, ITaskRepository, IDbInitializer)
│
├── TasksManagerCustom.Persistence/                 ← SQLite implementation
│   ├── DomainModels/  (Category, TaskDomainModel, Priority, Project, abstract bases)
│   ├── Queries/  (raw SQL strings: CategoryQueries, TasksQueries, DatabaseConstants)
│   ├── Repositories/ (AbstractRepository, CategoryRepository, TaskRepository)
│   ├── DbInitializer.cs                            ← Creates DB file and tables on first run
│   └── Constants.cs                                ← Table names, date formats, DB path
│
├── MenuBarModule/                                  ← Prism module: top toolbar (menu + icon buttons)
│   └── ViewModels/TopMenuBarViewModel.cs
│
├── TasksManagerCustom.LeftPanelModule/             ← Prism module: left navigation panel (categories tree)
│   └── ViewModels/LeftPanelSpaceViewModel.cs
│
├── TasksManagerCustom/Modules/TasksManagerCustom.Modules.ModuleName/  ← Prism module: tasks grid (center panel)
│   │   NOTE: still has placeholder module name — intended rename: TasksScheduleModule
│   └── ViewModels/TaskScheduleViewModel.cs
│
└── TasksManagerCustom/Tests/                       ← xUnit test project (mostly stubs at this point)
```

---

## Intentional Architectural Decisions

### Prism as MVVM framework
Prism provides DI (DryIoc), module system, region-based navigation, PubSub event aggregation,
and dialog service. All modules are loosely coupled via events and interfaces — a deliberate
learning goal for Prism's modularity features.

### Multi-layer DTO strategy
Three separate DTO types exist for each entity intentionally:

```
View model  ←→  Service DTO  ←→  PersistenceDTO  ←→  DomainModel (ORM entity)
```

- **Service DTOs** (`TasksManager.Services.Interfaces/DTOs/`) — the "business face" of data.
- **Persistence DTOs** (`TasksManager.PersistenceContracts/Dtos/`) — the persistence layer contract,
  decoupled from service contracts. Changing the DB schema only touches this layer.
- **Domain Models** (`TasksManagerCustom.Persistence/DomainModels/`) — sqlite-net ORM entities
  with SQLite-specific attributes.

AutoMapper bridges all three levels.

### Repository pattern for DB portability
`IRepository`, `ICategoryRepository`, `ITaskRepository` in `TasksManager.PersistenceContracts`
are the seam for swapping storage backends. Current implementation: SQLite.
Switching to MS SQL, PostgreSQL, or any other backend requires only a new implementation of
these interfaces + wiring in `RepositoryFactory` — no changes to service or view layers.

### RepositoryFactory (manual factory, not DI)
The persistence layer intentionally does **not** use Prism's DI container.
`RepositoryFactory<T>` uses a type-keyed dictionary to resolve repository implementations.
This is noted in the README ("DI for Service-Persistence Not Set") as a known design choice —
the intent is to keep the persistence layer fully standalone from the WPF/Prism infrastructure.

### SQLite for portability
No server required. The DB file lives in `<app_dir>/Data/database.db`.
The app is designed to be completely portable (xcopy deployment).

### Localization
Two language files: `Languages/en-US.xaml` and `Languages/ru-RU.xaml`.
Language is selected at startup in `App.SetLanguageDictionary()` based on `Thread.CurrentThread.CurrentCulture`.
All UI strings are DynamicResource bindings to these dictionaries.

---

## Data Flow (typical read)

1. User selects a category in LeftPanel  
2. `LeftPanelSpaceViewModel` publishes `CategoryOrProjectChangedEvent` via `IEventAggregator`  
3. `TaskScheduleViewModel` subscribes to this event, calls `ITasksQueryService.GetTasksListForCategory(ids)`  
4. `TasksQueryService` → `RepositoryFactory` → `TaskRepository.GetTasksByCategoriesIds()`  
5. Raw SQL → `SQLiteAsyncConnection.QueryAsync<TaskDomainModel>()` → `TaskRepository.ToDto()` → `PersistenceTaskDto` → `TaskDto`  
6. ViewModel maps `TaskDto` → `DataGridTaskModel` via `ToDataGridModel()`, binds to `DataGrid`

---

## Known TODOs (from code comments and current state)

- `AddUpdateTaskDialogViewModel` is a stub — `CanCloseDialog()` and `OnDialogClosed()` throw `NotImplementedException`
- Task creation via dialog not yet implemented (only Update exists in `ITaskRepository`)
- Colors for categories not implemented yet
- Update (edit) functionality for categories not implemented yet
- Logging not implemented (only `Debug.WriteLine`)
- Project navigation in left panel not implemented (only Category tree works)
- Tests are mostly stubs / commented out
- `RepositoriesEnum.cs` is currently dead code
- `Class1.cs` in `TasksManager.Services.Interfaces` is a leftover placeholder
- The `TasksManagerCustom.Modules.ModuleName` project has a placeholder name

---

## Conventions

- **MVVM**: ViewModels extend `BindableBase` (Prism). Commands are `DelegateCommand` or `DelegateCommand<T>`.
- **Events**: inter-module communication goes through `IEventAggregator` (PubSubEvent).
- **Dialogs**: Prism `IDialogService` / `IDialogAware` pattern.
- **Regions**: `RegionNames.cs` holds all region name constants.
- **Queries**: Raw SQL strings stored as constants in `Queries/` classes (no ORM query builder).
- **Naming**: Projects use `TasksManager.*` prefix; shared/cross-cutting use `TasksManagerCustom.*`.
- **async/await**: All persistence operations are async; service layer passes async through.
- **Null checks**: Always use `is null` and `is not null` for null comparisons. Never use `== null` or `!= null`.
- **DTOs**: Use plain classes with C# 12 Primary Constructors and get-only properties (no setters). Do **not** use `record` types anywhere in the project — DTOs are classes. Mapping between layers is done via direct instantiation and LINQ `.Select()`, not AutoMapper.
- **Date formatting**: Use `TasksManager.Shared.GlobalConstants.DateFormats` constants (`FullDateTime`, `ShortDate`). Use `TasksManager.Shared.Helpers.DateHelper.TryParseDate()` for string→DateTime? parsing.

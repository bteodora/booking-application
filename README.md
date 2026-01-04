<div align="center">
  <img src="logobooking.svg" alt="Booking System Logo" width="300"/>
  
  <br><br>

  <h1>Integrated Hospitality & Tourism Management System</h1>
  <h3>Desktop Application for Accommodation and Tour Lifecycle Management</h3>

  <p>
    <img src="https://img.shields.io/badge/Platform-.NET_6.0-512bd4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET"/>
    <img src="https://img.shields.io/badge/Framework-WPF-blue?style=for-the-badge&logo=windows&logoColor=white" alt="WPF"/>
    <img src="https://img.shields.io/badge/Architecture-MVVM-green?style=for-the-badge" alt="MVVM"/>
    <img src="https://img.shields.io/badge/Status-Production_Ready-red?style=for-the-badge" alt="Status"/>
  </p>

  <p>
    <strong>A robust, monolithic desktop solution engineered to digitize and automate the complex interactions between accommodation owners, guests, tour guides, and tourists.</strong>
  </p>

  <p>
    <a href="Specifikacija projekta.pdf"><strong>📄 View Official Specification (PDF) »</strong></a>
  </p>
</div>

---

## 1. Project Overview

The **Integrated Hospitality & Tourism Management System** (internally referenced as "Booking") is a high-fidelity desktop application developed to address the multifaceted needs of the modern tourism industry. Unlike generic booking platforms, this system provides a specialized, role-based environment that handles the complete lifecycle of accommodation rentals and tour management.

From complex reservation algorithms and conflict resolution to loyalty programs and renovation scheduling, the system offers a cohesive suite of tools. It is engineered with a strict adherence to **Clean Code principles**, **SOLID design patterns**, and the **Model-View-ViewModel (MVVM)** architectural pattern, ensuring scalability, testability, and maintainability.

## 2. Motivation and Background

The hospitality industry involves diverse stakeholders with conflicting technical proficiencies and distinct operational goals. Existing solutions often force a "one-size-fits-all" interface upon users, resulting in suboptimal efficiency for power users and accessibility barriers for novices.

### Design Goals
*   **Architectural Integrity:** To implement a strictly layered architecture that decouples business logic from presentation, utilizing dependency injection and the observer pattern to manage state.
*   **Persona-Driven Engineering:** To design distinct user interfaces (UI) and user experiences (UX) tailored specifically to the cognitive models and workflow requirements of four distinct demographic profiles.
*   **Data Consistency:** To maintain data integrity across a file-based persistence layer without the overhead of a relational database management system (RDBMS) for portability.

## 3. User-Centric Design & HCI Strategy

A defining characteristic of this system is its adherence to Human-Computer Interaction (HCI) principles. The application is not a single uniform interface but rather four distinct sub-systems, each optimized for a specific persona.

### The Guide (Vodič)
*   **Target Demographics:** Early 30s, digitally literate, efficiency-focused.
*   **UX Strategy:** Focused on business process automation. The interface is dense with information, utilizing dashboards for tour tracking and reservation management. It minimizes clicks and prioritizes rapid data entry and live status monitoring.

### The Tourist (Turista)
*   **Target Demographics:** Elderly individuals, low computer literacy, risk-averse.
*   **UX Strategy:** Prioritizes accessibility and error prevention. The interface features large action buttons (Fitts's Law), explicit instructional text, simplified navigation paths, and confirmation dialogs to reduce anxiety regarding accidental errors.

### The Accommodation Owner (Vlasnik)
*   **Target Demographics:** Experienced business owners, time-constrained.
*   **UX Strategy:** Designed for rapid management. Features high-level statistical overviews, batch processing capabilities for reservations, and automated notification systems to streamline property management tasks.

### The Guest (Gost)
*   **Target Demographics:** Transitional users accumulating digital skills, reliant on help systems.
*   **UX Strategy:** Intuitive navigation with discovered interfaces. The system utilizes wizards and tooltips to guide the user through complex booking flows, bridging the gap between novice and intermediate competency.

## 4. Key Features

The system is divided into four functional modules, each rich with specialized capabilities.

### 4.1 Accommodation Management (Owner)
*   **Property Lifecycle Management:** Registration of diverse property types (Apartments, Houses, Cottages) with granular attribute configuration including location, capacity, and cancellation policies.
*   **Advanced Analytics Engine:** A dedicated statistics dashboard utilizing `LiveCharts` to visualize occupancy rates, reservation history, and cancellation metrics on annual and monthly bases.
*   **Renovation Scheduling:** A smart scheduling system that identifies optimal renovation windows by analyzing reservation gaps, preventing conflicts with existing bookings.
*   **Review & Loyalty System:** A reciprocal review system. Owners achieve "Super-Owner" status based on aggregate scores (4.5+ average) and total reviews, granting visibility boosts within the search algorithm.
*   **Interactive Forums:** A location-based communication hub where owners can moderate discussions and provide verified information to potential guests.

### 4.2 Booking & Discovery (Guest)
*   **"Anywhere/Anytime" Search Algorithm:** A flexible search engine that suggests accommodations based on vague parameters (e.g., specific guest count and duration, but flexible dates), optimizing inventory utilization.
*   **Reservation Management:** Users can modify reservation dates. The system automatically validates availability and processes approval workflows through the Owner.
*   **Super-Guest Gamification:** A loyalty point system where users earn bonuses for maintaining active reservation streaks, redeemable for discounts on future bookings.
*   **Review Logic:** Guests can upload proof-of-stay images and rate cleanliness and rule adherence. The "double-blind" review visibility ensures objective feedback.

### 4.3 Tour Operations (Guide)
*   **Live Tour Tracking:** A real-time execution mode where guides mark key checkpoints. The system utilizes the **Observer Pattern** to broadcast progress to registered tourists instantly.
*   **Complex Tour Request Handling:** Logic to accept "Complex Tours" (multi-stage journeys). The system breaks down requests and allows guides to accept specific segments based on their availability.
*   **Statistical Reporting:** Comprehensive breakdowns of tour attendance by age demographics (Under 18, 18-50, 50+) and language frequency to assist in future tour planning.
*   **Voucher Generation:** Automated issuance of compensation vouchers in the event of tour cancellations, valid for one year.

### 4.4 Experience Booking (Tourist)
*   **Voucher Wallet:** A digital wallet for managing earned and compensation vouchers, with logic to auto-apply valid vouchers during the booking process.
*   **Tour Attendance Monitoring:** Real-time notifications alerting the tourist when their active tour has reached a specific checkpoint or when the guide has marked them as "present."
*   **Request System:** Tourists can submit requests for specific tour parameters (location, language). If no such tour exists, the system aggregates demand and notifies guides of high-potential opportunities.

## 5. Architecture and Design

The application is built upon a **Monolithic Layered Architecture**, strictly adhering to the **MVVM (Model-View-ViewModel)** pattern to ensure separation of concerns.

### High-Level Components
1.  **Presentation Layer (WPF):** Contains XAML Views and Code-behind (minimized). It binds to ViewModels via the `INotifyPropertyChanged` interface.
2.  **Application Layer (ViewModels):** Acts as the mediator. It processes user input, invokes business logic, and updates the View state.
3.  **Domain Layer (Services & Models):** Encapsulates the core business rules (e.g., "Is this date range available?", "Does this user qualify for Super status?").
4.  **Data Access Layer (Repositories):** Implements the **Repository Pattern**. It abstracts the underlying CSV file storage, providing a clean API (`Get`, `Add`, `Update`, `Delete`) to the upper layers.

### Design Patterns Implemented
*   **Dependency Injection (DI):** Managed via a custom `Injector` class to decouple dependencies and facilitate testing.
*   **Observer Pattern:** Heavily used for live updates (e.g., notifying a Tourist view when a Guide updates a tour point).
*   **Strategy Pattern:** implied in the calculation of varying discount strategies for loyalty programs.
*   **Singleton Pattern:** Used for global configuration and session management.

## 6. Technology Stack

### Core Technologies
*   **Language:** C# 10.0 / .NET 6.0+
*   **Framework:** Windows Presentation Foundation (WPF)
*   **Data Format:** CSV (Comma Separated Values) utilizing custom serialization logic.

### External Libraries (NuGet)
*   **WPF-UI / FontAwesome.Sharp:** For modern, polished UI components and iconography.
*   **LiveCharts.Wpf / OxyPlot:** For rendering high-performance financial and statistical charts.
*   **QuestPDF / iTextSharp / Syncfusion.Pdf:** Enterprise-grade PDF generation for reports and vouchers.
*   **MVVMLight:** For lightweight messaging and MVVM scaffolding.
*   **Extended.Wpf.Toolkit:** For advanced input controls (date pickers, numeric up/down).

## 7. Installation and Setup

### Prerequisites
*   Microsoft Windows 10 or 11
*   .NET 6.0 Runtime (or higher)
*   Visual Studio 2022 (recommended for development)

### Installation Steps
1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/bteodora/travel-manager-desktop-wpf.git
    cd booking-system
    ```
2.  **Restore Dependencies:**
    Open the solution file (`Booking.sln`) in Visual Studio. Right-click the solution and select **Restore NuGet Packages**.
3.  **Data Initialization:**
    Ensure the `Resources/Data` directory contains the initial CSV files. If strictly testing, the system comes pre-loaded with seed data in `user.csv`.
4.  **Build and Run:**
    Set the build configuration to `Debug` or `Release` and press `F5`.

## 8. Usage Guide

### Authentication
The system uses a unified login form that routes users to their specific dashboard based on the credentials provided in `Resources/Data/user.csv`.

**Example Credentials:**
*   **Owner:** `username: host1`, `password: host1`
*   **Guest:** `username: guest1`, `password: guest1`
*   **Guide:** `username: guide1`, `password: guide1`
*   **Tourist:** `username: tourist1`, `password: tourist1`

### Workflow Example: Booking an Accommodation
1.  Log in as **Guest**.
2.  Navigate to "Explore Accommodations."
3.  Use the filter sidebar to select "City: Paris" and "Type: Apartment."
4.  Select a property and click "Reserve."
5.  Enter dates. If dates are taken, the system will suggest the nearest available alternative.
6.  Confirm booking. The logic updates `reservations.csv` and decrements availability.

## 9. Project Structure

The solution is organized to reflect the architectural layers:

*   **`Application`**: Contains core business logic services (`ReservationServices`, `RateServices`).
*   **`Domain`**: Holds the POCO (Plain Old CLR Object) models and interface definitions.
*   **`Repository`**: Implementation of data persistence, reading/writing to CSVs.
*   **`WPF`**: The presentation layer.
    *   `Views/Windows`: Top-level windows.
    *   `Views/Pages`: Navigable content areas.
    *   `ViewModels`: Glue code between Views and Models.
    *   `Resources`: XAML styles, converters, and assets.
*   **`Observer`**: Custom implementation of the reactive update mechanism.
*   **`Utilities`**: Helper classes for PDF generation and Theme switching.

## 10. Configuration

Configuration is primarily handled via `app.config` and the `Properties` directory.
*   **Localization:** The system supports localization infrastructure located in `LocalisationResources`.
*   **Theming:** Light/Dark mode preferences are persisted in user settings and applied at runtime via `WPF/Styles`.

## 11. Testing & Quality Assurance

While the current distribution relies on manual system testing, the architecture is designed for testability.
*   **Unit Testing:** The `Service` layer is decoupled from the UI, allowing for NUnit/xUnit tests to verify business rules (e.g., cancellation penalty calculations) in isolation.
*   **Defensive Programming:** The application employs strict input validation (Data Annotations and custom logic) to prevent corruption of the CSV data store.

## 12. Security Considerations

*   **Password Storage:** While currently stored in CSV for academic demonstration, the architecture supports replacing the `UserRepository` with a secure implementation hashing passwords (e.g., BCrypt) without altering business logic.
*   **Data Validation:** All user inputs are sanitized to prevent injection attacks or format errors during serialization.

## 13. Limitations

*   **Concurrency:** As a file-based system, concurrent writes from multiple instances may cause race conditions. This is a known limitation of the CSV persistence layer intended for single-client demonstration.
*   **Scalability:** Performance may degrade with datasets exceeding 100,000 records due to in-memory loading of CSV files.

## 14. Roadmap

*   **Migration to SQL:** Implementing an Entity Framework Core repository to replace CSV storage.
*   **Cloud Integration:** moving the backend logic to an ASP.NET Core Web API.
*   **Mobile Companion App:** Developing a MAUI app for Tourists to use strictly for ticket validation and notifications.


## 15. Gallery

### Guide Interface
<p float="left">
  <img src="Screenshots/guidemain.png" alt="Guide Main" width="45%" />
  <img src="Screenshots/guidestats.png" alt="Guide Stats" width="45%" /> 
</p>

### Owner Interface
<p float="left">
  <img src="Screenshots/hostmain.png" alt="Host Main" width="45%" />
  <img src="Screenshots/hoststats.png" alt="Host Stats" width="45%" />
</p>

### Guest Interface
<p float="left">
  <img src="Screenshots/guestmain.png" alt="Guest Light" width="45%" />
  <img src="Screenshots/guestmaindark.png" alt="Guest Dark" width="45%" />
</p>

### Tourist Interface
<p float="left">
  <img src="Screenshots/touristmain.png" alt="Tourist Main" width="45%" />
  <img src="Screenshots/touristrequests.png" alt="Tourist Requests" width="45%" />
</p>

### Login Window
<img src="Screenshots/login.png" alt="Login Window" width="670">



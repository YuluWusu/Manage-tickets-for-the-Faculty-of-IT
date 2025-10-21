# 🎓 RESOLUTION REQUEST MANAGEMENT SYSTEM (HUIT Priority Queue)

[](https://dotnet.microsoft.com/)
[](https://docs.microsoft.com/en-us/dotnet/csharp/)

## 📝 Project Description

This project is a Command-Line Interface (CLI) Console application simulating a **System for Managing and Processing Student Resolution Requests (Phiếu Giải Quyết)**, typically found in a University context (HUIT). [cite\_start]The system uses the **Priority Queue** data structure to automatically sort and prioritize requests based on their content[cite: 3].

[cite\_start]The priority level of each request is automatically calculated by the `PriorityService` based on a predefined list of urgent or important keywords found within the request's content[cite: 3].

## ✨ Key Features

  * [cite\_start]**Priority Queue Implementation:** Manages requests (`PhieuGiaiQuyet`) with three priority levels: **HIGH (2)**, **MEDIUM (1)**, and **LOW (0)**[cite: 3].
  * **CRUD Operations:**
      * Add a new request (`Enqueue`).
      * Remove and process the highest-priority request (`Dequeue`).
      * Update an existing request based on Student ID.
      * Search for requests by Student ID.
  * **File Handling:** Supports reading a list of requests from a file (`.txt`) and writing the updated list back to a file.
  * **Statistics:**
      * Generates statistics on the class with the highest number of requests.
      * Generates statistics on the distribution of priority levels (HIGH, MEDIUM, LOW).
  * **Display:** Shows the top 10 requests next in line for processing.

## 🚀 Getting Started

### System Requirements

  * **.NET Framework** (Version `v4.7.2` or later).
  * Development environment: Visual Studio (or Visual Studio Code with the .NET SDK).

### Installation and Run

1.  **Clone Repository:**

    ```bash
    git clone [YOUR_GITHUB_REPO_LINK]
    cd HUIT_PriorityQueue
    ```

2.  **Open and Build:**

      * Open the project in Visual Studio.
      * Build (Ctrl+Shift+B) and Run (F5).

3.  **Using Data File (Optional):**

      * The application can read a sample data file named `ds_PhieuGiaiQuyet.txt`.
      * Use option **"2. Đọc danh sách từ file" (Read list from file)** to load data into the queue.

## ⚙️ Code Structure

| File/Module Name | Purpose |
| :--- | :--- |
| `Program.cs` | Application entry point; initializes and runs the Console UI. |
| `ConsoleUI.cs` | Handles the command-line user interface, displays the Menu, and manages all 12 interactions/functions. |
| `PriorityQueue.cs` | **Core Data Structure:** Implements the Priority Queue using a linked list. |
| `PriorityService.cs` | [cite\_start]Responsible for calculating the priority level of a request based on keyword scanning[cite: 3]. |
| `FileService.cs`, `IFileService.cs` | [cite\_start]Manages reading and writing request data to/from files[cite: 2]. |
| `PhieuGiaiQuyet.cs` | Defines the data models (`PhieuGiaiQuyet`, `NgayThangNam`). |

## 🤝 Contributing

If you find any bugs or have suggestions for improvements:

1.  Fork this repository.
2.  Create a new branch for your feature/fix (`git checkout -b feature/AmazingFeature`).
3.  Commit your changes (`git commit -m 'Add AmazingFeature'`).
4.  Push to the branch (`git push origin feature/AmazingFeature`).
5.  Open a **Pull Request**.

## 📄 License

This project is licensed under the **MIT License**. See the `LICENSE` file for more details.

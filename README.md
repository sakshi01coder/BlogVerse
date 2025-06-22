# 📚 BlogVerse - ASP.NET MVC Blogging Platform

**BlogVerse** is a full-featured blogging web application built using **ASP.NET Core MVC**, designed to empower writers and readers to connect through meaningful content.

---

## 🚀 Features

### 👤 User Features
- 📝 **Register & Login** (with password hashing)
- 🌗 **Dark/Light Mode Toggle**
- 📖 **Explore Blogs** by categories
- ❤️ **Like, Comment, and Save** blogs
- 📂 **Reading History** and **Saved Blogs**
- 🔍 **Search and Filter** by topic or author
- 🔔 **Get Notified** when favorite authors post

### ✍️ Writer Features
- 🧠 **AI-based Blog Ideas** (optional future feature)
- ✨ **Rich Text Editor** (TinyMCE integrated)
- 📊 **Writer Dashboard**
  - My Posts
  - Saved Drafts
  - Analytics (e.g., views, likes, category-wise charts)

### 📢 Homepage Highlights
- 🦄 Hero section with CTA
- 🔥 Trending Blogs (interactive with detail view + popup)
- 🎯 "Why BlogVerse?" benefits grid
- 🛎️ Pop-up trigger after 30s on blog detail pages

---

## 🛠️ Tech Stack

| Layer        | Technology                         |
|--------------|-------------------------------------|
| Frontend     | HTML, CSS, Bootstrap, JavaScript    |
| Backend      | ASP.NET Core MVC (C#)               |
| Database     | SQL Server (via Entity Framework)   |
| Auth System  | ASP.NET Core Identity               |
| Rich Editor  | TinyMCE                             |

---

## 🏗️ Project Setup

### ✅ Prerequisites
- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or VS Code
- SQL Server

### 🔧 Setup Commands

```bash
# Restore packages
dotnet restore

# Apply migrations
dotnet ef database update

# Run the app
dotnet run!

[image](https://github.com/user-attachments/assets/3d3bd2a3-fa7e-45e0-8a9e-cd929d3d6cec)


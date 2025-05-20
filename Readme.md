## 🚀 How to Use This Template

### 1. Clone the Template Repository

Start by cloning this repository to your local machine using Git:

```bash
git clone https://github.com/xantric/SecyTask_GameDev.git
```

### 2. Navigate Into the Project Folder

```bash
cd SecyTask_GameDev
```
### 3. Remove the Old Git Remote

Disconnect the original remote repository to avoid pushing to it:

```bash
git remote remove origin
```

### 4. Create a New GitHub Repository
Go to https://github.com and create a new empty repository on your account.

### 5. Add Your GitHub Repository as the New Remote
Example: (Use your own link after origin)

```bash
git remote add origin https://github.com/yourusername/unity-assignment.git
```
Verify that the new origin was added:

```bash
git remote -v
```

#### Now you can normally use git in your project (push,pull,etc.)
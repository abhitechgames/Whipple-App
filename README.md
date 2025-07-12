# PREPARE – The Whipple Surgery Companion App

**PREPARE** is a Unity-based mobile health app designed to support patients undergoing the Whipple procedure (pancreaticoduodenectomy). The app helps users track daily vitals, log exercises, manage pre-surgery routines, and get educated with easy-to-understand resources.



---

### 📦 Project Setup Instructions

Follow these steps to download, configure, and run the PREPARE app in Unity.

---


#### 1. Clone the Repository

```bash
git clone https://github.com/abhitechgames/prepare-whipple.git
```

#### 2. Open the Project in Unity

- Open Unity Hub
- Click **Add** and select the cloned project folder
- Make sure you're using **Unity 2022.3 LTS** or later
- Set the build target to **Android**

---

#### 3. Firebase Setup

To enable features like data logging, real-time updates, and patient record sync, the app uses **Firebase Realtime Database**.

##### ✅ Create a Firebase Project

- Go to: https://console.firebase.google.com
- Click **"Add Project"**
- Follow the prompts and finish project creation

##### 🔧 Add Firebase to Your Unity Android Project

**In Firebase Console:**
- Click **"Add App" → Android**
- Enter your Unity Android package name (e.g., `com.yourcompany.prepare`)
- Download the `google-services.json` file

**In Unity:**
- Place `google-services.json` in `Assets/`
- Install the **Firebase Unity SDK** (from https://firebase.google.com/docs/unity/setup)
- Import these packages:
  - FirebaseDatabase

---

#### 4. Enable Realtime Database

**In Firebase Console:**
- Go to **Build > Realtime Database**
- Click **Create Database**
- Go to rules and set **Read** and **Write** to **True**

- Copy your Firebase Realtime Database URL

**In Unity:**
- Use this URL while initializing or accessing Firebase

---

#### 5. Build & Run

- Connect your Android device (ensure **Developer Mode** is enabled)
- In Unity:
  - Go to `File > Build Settings > Android > Build and Run`

---

#### 📩 Questions or Support?

- Email us at: **preparewhipple@gmail.com**


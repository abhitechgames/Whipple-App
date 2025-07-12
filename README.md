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

  <img width="220" height="190" alt="image" src="https://github.com/user-attachments/assets/eb99bc67-c5b8-4669-99fa-5441c3d5abee" />

##### 🔧 Add Firebase to Your Unity Android Project

**In Firebase Console:**
- Click **"Add App" → Unity Project**
- Enter your Unity Android package name (e.g., `com.yourcompany.prepare`)
- Download the `google-services.json` file

  <img width="531" height="257" alt="image" src="https://github.com/user-attachments/assets/c2be37be-3f3c-473c-9f77-ee45d1fae24c" />
  <img width="178" height="93" alt="image" src="https://github.com/user-attachments/assets/d980c456-3da3-404a-8d11-2071a57fbb1f" />
  <img width="775" height="495" alt="image" src="https://github.com/user-attachments/assets/497fcbdb-0b5d-43d5-858b-a06f5838043c" />
  <img width="712" height="742" alt="image" src="https://github.com/user-attachments/assets/063dd457-f194-42a9-a36d-2ec095918371" />
  <img width="1222" height="318" alt="image" src="https://github.com/user-attachments/assets/cd714ab6-3ccd-4284-b942-43d873d76868" />


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

  <img width="314" height="672" alt="image" src="https://github.com/user-attachments/assets/6cab165c-3598-423f-b592-ac4934936079" />
  <img width="539" height="467" alt="image" src="https://github.com/user-attachments/assets/f5a6fa30-233d-4fbd-b0fe-b40d619ab16c" />
  <img width="1042" height="465" alt="image" src="https://github.com/user-attachments/assets/fee38f96-49fe-4051-abb2-c6fc3028e30d" />


---

#### 5. Build & Run

- Connect your Android device (ensure **Developer Mode** is enabled)
- In Unity:
  - Go to `File > Build Settings > Android > Build`

---

#### 📩 Questions or Support?

- Email us at: **preparewhipple@gmail.com**


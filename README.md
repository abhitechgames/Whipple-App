# Prepare - Whipple
### The Whipple Surgery Companion App
**PREPARE** is a Unity-based mobile health app designed to support patients undergoing the Whipple procedure (pancreaticoduodenectomy). The app helps users track daily vitals, log exercises, manage pre-surgery routines, and get educated with easy-to-understand resources.


<a href = "https://abhitechgames.itch.io/prepare-whipple"><img width="90" height="90" alt="pancreas" src="https://github.com/user-attachments/assets/a70a4330-4eb7-42c8-b711-57889587e268"/></a>

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
- Go to rules and set **Read** and **Write** to **True** & publish the changes.

  <img width="314" height="672" alt="image" src="https://github.com/user-attachments/assets/6cab165c-3598-423f-b592-ac4934936079" />
  <img width="1042" height="465" alt="image" src="https://github.com/user-attachments/assets/fee38f96-49fe-4051-abb2-c6fc3028e30d" />


---

#### 5. Build & Run

- Connect your Android device (ensure **Developer Mode** is enabled)
- In Unity:
  - Go to `File > Build Settings > Android > Build`

---

#### 📩 Questions or Support?

- Email us at: **preparewhipple@gmail.com**

---

I made this app (though I'm a game dev) to help pancreatic cancer patients and their caretakers - track daily activities like Diet and Physio before a major surgery like the Whipple Procedure (pancreaticoduodenectomy)

My fatherfigure uncle recently got diagnosed with Pancreatic Ductal Adenocarcinoma in April 2025, he went through the Whipple Procedure on July 4th, 2025.

Edit:
My father-figure uncle passed away yesterday at just 48 years old, within a year of undergoing the Whipple Procedure. I still cannot believe it.

He always took great care of his health - regular walks, exercise, and a disciplined lifestyle. Some of my favorite memories are the rides with him to the market to buy groceries for the household. He was also the reason I learned how to ride a bike. My father was always against me riding one, but my uncle trusted me with his bike. He leaves behind his wife and two young daughters, just 19 and 13 years old. I do not fully know how I will cope with this loss yet, but I truly hope he is at peace wherever he is.

please keep him in your good wishes and prayers...

**Thank you!**

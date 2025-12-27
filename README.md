<div align="center">

# 🏺 Tomb of the Forgotten Architect

### *An Immersive VR Escape Room Experience*

[![Unity 6](https://img.shields.io/badge/Unity-6-black?logo=unity)](https://unity.com/)
[![Meta Quest](https://img.shields.io/badge/Meta-Quest-0467DF?logo=meta)](https://www.meta.com/quest/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![XR Toolkit](https://img.shields.io/badge/XR%20Toolkit-000000?style=for-the-badge&logo=unity&logoColor=white)](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@latest)

*A course graduation project showcasing VR game development, interactive puzzle design, and immersive storytelling*

[🎮 Game Design Document](https://careful-daughter-e76.notion.site/Tomb-of-the-Forgotten-Architect-29e526d4858d8040a70af5fb44bd08c0) • [📊 UML Diagrams](https://acupofj0e.github.io/escape_room_depi/) • [🔧 Installation](#-installation)

</div>

---

## 📖 Overview

**Tomb of the Forgotten Architect** is a virtual reality escape room set in an ancient Egyptian tomb. Players must solve three interconnected puzzles to assemble a mystical key and escape before the tomb seals forever. Built for Meta Quest 2 using Unity and XR Interaction Toolkit, this project demonstrates advanced VR mechanics, event-driven architecture, and polished game design.

### 🎯 Core Features

- **🧩 Three Unique Puzzles** - Pyramid Diorama, Hieroglyphic Cipher, and Balance Scale challenges
- **🎨 Authentic Egyptian Atmosphere** - Detailed tomb environment with dynamic lighting and spatial audio
- **✋ Natural VR Interactions** - Physics-based object manipulation using Meta Quest 2 controllers
- **🔊 Immersive Audio** - 3D spatial sound with footsteps, object impacts, and environmental ambience
- **🎬 Cinematic Sequences** - Dramatic key assembly and door opening animations
- **📐 Modular Architecture** - Event-driven design with reusable components and managers

---

## 🎥 Live Demo

<div align="center">

[![Watch the Demo](https://img.youtube.com/vi/gdSh4rQNCFI/maxresdefault.jpg)](https://youtu.be/gdSh4rQNCFI)

**[▶️ Watch Full Gameplay Demo on YouTube](https://youtu.be/gdSh4rQNCFI)**

*Experience the immersive VR gameplay, puzzle mechanics, and atmospheric tomb environment in action!*

</div>

---

## 🎮 Gameplay

<div align="center">

### The Challenge

*You've discovered the tomb of an ancient architect who vanished mysteriously thousands of years ago. To escape, you must solve the three sacred puzzles and reassemble the Architect's Key.*

</div>

### 🏛️ The Three Puzzles

#### 1. **Pyramid Diorama** 🔺
Place three pyramid models representing Khufu, Khafre, and Menkaure in their correct positions on the Giza plateau. Each pyramid must match its corresponding slot to unlock the first key fragment.

#### 2. **Balance Scale** ⚖️
Achieve perfect equilibrium by carefully distributing ancient weights across two plates. The scales physically respond to mass, providing immediate visual feedback. Balance unlocks the second fragment.

#### 3. **Hieroglyphic Cipher** 📜
Decode hieroglyphic symbols scattered throughout the tomb and place stone tablets in their correct wall slots. When all tablets align, they snap into place with satisfying precision, revealing the final fragment.

### 🔑 The Final Escape

Once all three fragments are collected, assemble the complete key at the central pedestal. Watch as the key rises dramatically, puzzle room doors seal, and the massive tomb entrance opens in a stunning two-stage animation. Pass through the doorway to complete your escape!

---

## 🛠️ Technical Architecture

### System Design

Our game uses a **modular event-driven architecture** with clear separation of concerns:

- **Puzzle Managers** - Central controllers for each puzzle (`room1_Manager`, `Room3_Manager`, `jigsaw_manager`)
- **Object Handlers** - Component-level logic (`pyramidSlot`, `tabletHandler`, `MassHandler`)
- **Event System** - Unity Events for loose coupling between systems
- **Key Assembly** - Central hub connecting all puzzles to the finale (`keyplacment`, `keyplaceariser`)
- **Audio Engine** - Dynamic spatial audio with collision-based sound effects

### 📊 Architecture Documentation

Our complete system architecture is documented with professional UML diagrams:

<div align="center">

**[📊 View Complete UML Diagrams →](https://acupofj0e.github.io/escape_room_depi/)**

</div>

The documentation includes:
- Complete System Class Diagram
- Puzzle Sequence Diagrams (all 3 puzzles + key assembly)
- Component Architecture & State Flow
- Event-Driven Architecture Visualization
- Audio System Design
- Deployment Diagram

### 🎯 Design Patterns Implemented

| Pattern | Usage | Implementation |
|---------|-------|----------------|
| **Observer** | Event communication | Unity Events (`UnityAction`) for puzzle completion callbacks |
| **Manager** | Puzzle state control | Centralized managers for each puzzle system |
| **Component** | Modular design | Unity GameObject-Component architecture |
| **State** | Validation logic | Boolean flags and counters for puzzle progress |
| **Strategy** | Puzzle mechanics | Different validation approaches per puzzle type |
| **Coroutine** | Smooth animations | Non-blocking door/key animations with interpolation |

### 🏗️ Tech Stack

- **Engine**: Unity 2022.3 LTS
- **VR Platform**: Meta Quest 2 (Android/ARM64)
- **VR Framework**: Unity XR Interaction Toolkit
- **Language**: C# 
- **Physics**: Unity Physics Engine (Rigidbody, Colliders)
- **Audio**: Unity Audio Engine with 3D spatial audio
- **Version Control**: Git with Unity Plastic SCM

---

## 🚀 Installation

### Prerequisites

- Unity 2022.3 LTS or newer
- Meta Quest 2 headset
- Meta Quest Link cable or Air Link setup
- Git or GitHub Desktop

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/ACupOfJ0e/escape_room_depi.git
   cd escape_room_depi
   git checkout shihab
   ```

2. **Open in Unity Hub**
   - Launch Unity Hub
   - Click "Add" and select the cloned project folder
   - Open with Unity 2022.3 LTS

3. **Configure XR Settings**
   - Navigate to `Edit > Project Settings > XR Plug-in Management`
   - Enable **Oculus** provider (Meta Quest 2)
   - Verify XR Interaction Toolkit is properly imported

4. **Build for Meta Quest 2**
   - Connect your Quest 2 via Link cable or enable Air Link
   - Go to `File > Build Settings`
   - Switch platform to **Android**
   - Select your Quest 2 device
   - Click **Build and Run**

5. **Play!**
   - Put on your headset and start solving puzzles
   - Use the grip buttons to grab objects
   - Use the joysticks for locomotion

---

## 🎓 Project Structure

```
escape_room_depi/
├── Assets/
│   ├── Scenes/              # Main game scene
│   ├── Scripts/             # C# gameplay scripts
│   │   ├── Managers/        # Puzzle managers
│   │   ├── Handlers/        # Object handlers
│   │   ├── Audio/           # Sound effects scripts
│   │   └── Utilities/       # Helper scripts
│   ├── Prefabs/             # Reusable game objects
│   ├── Materials/           # Textures & shaders
│   ├── Audio/               # Sound effects & music
│   └── XR/                  # VR interaction setups
├── Packages/                # Unity packages
├── ProjectSettings/         # Unity project config
├── Builds/                  # Build output
└── docs/                    # UML diagrams source
```

---

## 👥 Team

This project was created as a course graduation project by:

<div align="center">

| Developer | Role | Contributions |
|-----------|------|---------------|
| **Youssef Hatem** | Game Designer | Core Puzzle Design, Documentation Author, UML Designer |
| **Mohamed Amr** | Techincal Artist | Assets, Environments, Audio Systems |
| **Shihab Rehan** | Developer | Core Puzzle Logic, Physics/VR Interactions, Architecture |

</div>

---

## 📚 Documentation

### Game Design
Our comprehensive Game Design Document covers narrative, mechanics, level design, and player experience:

<div align="center">

**[📖 Read the Full GDD →](https://careful-daughter-e76.notion.site/Tomb-of-the-Forgotten-Architect-29e526d4858d8040a70af5fb44bd08c0)**

</div>

### Technical Documentation
Complete UML diagrams and system architecture documentation:

<div align="center">

**[📊 View UML Diagrams →](https://acupofj0e.github.io/escape_room_depi/)**

</div>

Includes:
- Class diagrams for all major systems
- Sequence diagrams showing puzzle flows
- State diagrams for game progression
- Component architecture visualization
- Design pattern implementations

---

## 🎨 Key Technical Highlights

### Event-Driven Architecture
Heavy use of Unity Events enables loose coupling between puzzle components and managers, making the system highly modular and extensible.

### Physics-Based Interactions
Leverages Unity's physics system (Rigidbody, Colliders) for natural VR object manipulation and collision detection that feels realistic in VR.

### Coroutine-Based Animation
Smooth, non-blocking animations for doors, keys, and objects using Unity coroutines with interpolated movement over time.

### Dynamic Audio System
Runtime AudioSource creation with spatial audio (spatialBlend = 1) for immersive 3D sound effects that react to player actions.

### Progressive Challenge Design
The key assembly system creates a satisfying climax by unifying all three puzzle solutions into a dramatic escape sequence with cinematic flair.

---

## 🎯 Learning Outcomes

This project demonstrates proficiency in:

- ✅ VR game development for Meta Quest 2
- ✅ Unity XR Interaction Toolkit implementation
- ✅ Event-driven architecture and design patterns
- ✅ Physics-based gameplay mechanics
- ✅ 3D spatial audio integration
- ✅ Coroutine-based animation systems
- ✅ Modular code architecture
- ✅ Game state management
- ✅ UML documentation and system design
- ✅ Version control with Git

---

## 🔮 Future Enhancements

Potential improvements for future iterations:

- 🌐 **Multiplayer Support** - Cooperative puzzle solving with multiple players
- 🏆 **Scoring System** - Time-based challenges and leaderboards
- 🎭 **Additional Rooms** - Expand the tomb with more puzzles and chambers
- 🗣️ **Narrative Elements** - Voice acting and environmental storytelling
- 💾 **Save System** - Progress persistence across play sessions
- 🎨 **Visual Polish** - Enhanced particle effects and shader work
- 🎵 **Original Soundtrack** - Custom Egyptian-themed music composition

---

## 📄 License

This project is developed as a course graduation project. All rights reserved.

---

## 🙏 Acknowledgments

- **Unity Technologies** for the Unity Engine and XR Interaction Toolkit
- **Meta** for Meta Quest 2 development platform and documentation
- **Our Instructors** for guidance throughout the development process
- **Egyptian Heritage** for inspiring the game's setting and atmosphere

---

<div align="center">

### 🏺 Built with passion for immersive VR experiences 🏺

**[⬆ Back to Top](#-tomb-of-the-forgotten-architect)**

*December 2025*

</div>

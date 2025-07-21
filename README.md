# 🔬 VR Cell Explorer

**VR Cell Explorer** is an immersive Unity VR educational game that helps users learn about **Plant** and **Animal** cells by exploring and interacting with 3D cellular structures in a virtual environment.

---

## 🎮 Features

- 🧫 **Choose Your Cell Type**  
  Begin by selecting either a **Plant Cell** or **Animal Cell** to explore.

- 👻 **Ghost vs Real Materials**  
  Ghost-like placeholder materials are visually replaced with correct materials once the user identifies or interacts correctly. This teaches recognition through material transformation.

- 🔄 **Material Mapping System**  
  Materials are swapped using a robust mapping logic:
  - Ghost Material → Correct Material  
  - No mapping? A warning is logged for debugging.

- 🧠 **Interactive Learning**  
  Players learn about each organelle's name and function through gaze-based or pointer-based interaction in VR.

- 🎥 **Cinematic Introduction Panel**  
  Optional video panel to display tutorials or educational intros before exploration begins.

- 🔍 **Zoom and Inspect**  
  Users can zoom into parts of the cell and inspect specific organelles in detail.

- 👁 **Eye-Tracking / VR Gaze Input Support**  
  Designed for hands-free input — look at a component to interact.

- 📦 **Component Breakdown Panel**  
  When an organelle is selected, a UI panel shows:
  - Name of the part  
  - Description  
  - Whether it exists in plant, animal, or both cell types.

---

## 🧩 Gameplay Flow

1. **Home Panel** – Choose between *Plant* and *Animal* Cell.
2. **Cell View Panel** – Explore the cell in full 3D.
3. **Interaction** – Look at parts or click to trigger material correction and learning info.
4. **About Panel** – Learn more about the game and its purpose.
5. **Quiz Mode (optional)** – Test your memory by identifying cell parts.

---

## 🧰 Developer Notes

- All ghost materials are replaced using a centralized `MaterialReplacementManager.cs`.
- VR camera and gaze pointer handled via Unity's XR Interaction Toolkit.
- All interactions are modular and easy to extend.
- Compatible with Oculus and OpenXR.

---

## 📂 Folder Structure

Assets/
├── Scripts/
│ ├── GameManager.cs
│ ├── MaterialReplacementManager.cs
│ └── CellInteraction.cs
├── Materials/
│ ├── Ghost/
│ └── Correct/
├── Prefabs/
│ ├── PlantCell/
│ └── AnimalCell/

---

## 🛠 Requirements

- Unity 2021.3 LTS or later
- XR Plugin Management
- Oculus Integration or OpenXR
- VR-ready PC or headset (Meta Quest, Rift, HTC Vive, etc.)

---

## 📚 Educational Goal

This game is designed for middle-school to high-school students, biology learners, and VR classrooms to enhance spatial understanding of cellular structures.

---

## 🧑‍💻 Contributions

Feel free to open an issue or PR if you'd like to contribute, report bugs, or suggest features!

---

## 📜 License

This project is licensed under the MIT License.

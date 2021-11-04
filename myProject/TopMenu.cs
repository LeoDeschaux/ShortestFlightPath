using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine;

namespace zzMathVisu
{
    public class TopMenu
    {
        public TopMenu()
        {

        }

        public void DrawGUI()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("Edit"))
                {
                    if (ImGui.MenuItem("Refresh Scene"))
                    {
                        SceneManager.ReloadScene();
                        return;
                    }
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("Change Scene"))
                {
                    if (ImGui.MenuItem("01 - Trig Circle"))
                    {
                        SceneManager.ChangeScene(typeof(Scene_TrigCircle));
                        return;
                    }
                    if (ImGui.MenuItem("02 - Earth 2D - Pins"))
                    {
                        SceneManager.ChangeScene(typeof(Scene_Earth2DPins));
                        return;
                    }
                    if (ImGui.MenuItem("03 - Earth 2D - Path"))
                    {
                        SceneManager.ChangeScene(typeof(Scene_Earth2DPath));
                        return;
                    }
                    if (ImGui.MenuItem("04 - Earth 3D - Sphere Coordinates"))
                    {
                        SceneManager.ChangeScene(typeof(Scene_SphereCoordinates));
                        return;
                    }
                    if (ImGui.MenuItem("05 - Earth 3D - Paths"))
                    {
                        SceneManager.ChangeScene(typeof(Scene_EarthGlobe));
                        return;
                    }
                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }
    }
}

using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Desktop
{
    public class MaterialUI
    {
        public static void loadMaterial(MaterialForm actualMaterialForm)
        {
            //Create a material theme manager and add the form to manager (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(actualMaterialForm);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            //Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }
    }
}

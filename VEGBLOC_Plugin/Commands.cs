using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;


[assembly: CommandClass(typeof(VEGBLOC_Plugin.Commands))]

namespace VEGBLOC_Plugin
{
    public class Commands
    {
        Internal InternalFunction = new Internal();

        [CommandMethod("vegbloc")]
        public void vegbloc()
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;
            InternalFunction.Manual_Create_VEG_Block();
        }

        [CommandMethod("vegbloc_from_list")]
        public void vegbloc_from_list()
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;
            InternalFunction.Auto_Create_VEG_Block();
        }


    }
}

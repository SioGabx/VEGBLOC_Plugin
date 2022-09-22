using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Windows.Forms;

namespace VEGBLOC_Plugin
{
    public class Internal
    {
        Document doc = AcAp.DocumentManager.MdiActiveDocument;
        //string layer = (string)AcAp.GetSystemVariable("clayer");
        DBObjectCollection ents = new DBObjectCollection();


        public void Auto_Create_VEG_Block()
        {
            using (var dialog = new Internal_modal_table())
            {
                dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                var dlgResult = AcAp.ShowModalDialog(dialog);

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    /*DialogResult result = MessageBox.Show("Voullez-vous crée une légende pour chaqu'un des végétaux ?","VEGBLOC - Légendes", MessageBoxButtons.YesNo);
                    bool createlegende = false;
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        createlegende = true;
                    }*/
                    bool createlegende = true;
                    
                    DataGridView data = dialog.global_datatable;
                    foreach (DataGridViewRow row in data.Rows)
                    {
                        string nom_vegetal = (string)row.Cells["datatable_nom_latin"].Value;
                        string type_vegetal = (string)row.Cells["datatable_vegetation_type"].Value;
                        string color = (string)row.Cells["datatable_color"].Value;
                        double hauteur_vegetal = (double)Convert.ToDouble(row.Cells["datatable_veg_hauteur"].Value); 
                        double largeur_vegetal = (double)Convert.ToDouble(row.Cells["datatable_veg_largeur"].Value) / 2;
                        Create_VEG_Block(RemoveSpecialCharacters(nom_vegetal), RemoveSpecialCharacters(type_vegetal), hauteur_vegetal, largeur_vegetal, createlegende, color);
                    }


                }
            }
        }

        public void Manual_Create_VEG_Block()
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            //var db = doc.Database;
            var ed = doc.Editor;
            using (var dialog = new Internal_modal())
            {
                dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                var dlgResult = AcAp.ShowModalDialog(dialog);
                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    string nom_vegetal = dialog.nom_vegetal;
                    string type_vegetal = dialog.type_vegetal;
                    double hauteur_vegetal;
                    double largeur_vegetal;
                    bool create_legende = dialog.create_legende;
                    try
                    {
                        hauteur_vegetal = ((double)dialog.hauteur_vegetal);
                    }
                    catch (System.Exception)
                    {
                        hauteur_vegetal = 0;
                        ed.WriteMessage("Erreur de conversion de la hauteur du végétal " + nom_vegetal + "\n");
                    }
                    try
                    {
                        largeur_vegetal = ((double)dialog.largeur_vegetal);
                    }
                    catch (System.Exception)
                    {
                        largeur_vegetal = 0;
                        ed.WriteMessage("Erreur de conversion de la largeur du végétal " + nom_vegetal + "\n");
                    }




                    Create_VEG_Block(RemoveSpecialCharacters(nom_vegetal), RemoveSpecialCharacters(type_vegetal), hauteur_vegetal, largeur_vegetal, create_legende, null);
                }
            }




        }

        
        public void Create_VEG_Block(string nom_vegetal, string type_vegetal, double hauteur_vegetal, double largeur_vegetal, bool create_legende, string rgbstring = null)
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;

            ents.Clear();
            /*using (var dialog = new Internal_modal())
            {
                dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                var dlgResult = AcAp.ShowModalDialog(dialog);
                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    string nom_vegetal = dialog.nom_vegetal;
                    string type_vegetal = dialog.type_vegetal;
                    double hauteur_vegetal;
                    double largeur_vegetal;
                    bool create_legende = dialog.create_legende;
                    try
                    {
                        hauteur_vegetal = ((double)dialog.hauteur_vegetal);
                    }
                    catch (System.Exception)
                    {
                        hauteur_vegetal = 0;
                    }
                    try
                    {
                        largeur_vegetal = ((double)dialog.largeur_vegetal);
                    }
                    catch (System.Exception)
                    {
                        largeur_vegetal = 0;
                    }
            */

            if (String.IsNullOrEmpty(nom_vegetal) || String.IsNullOrEmpty(type_vegetal) || hauteur_vegetal < 0 || largeur_vegetal <= 0)
            {
                ed.WriteMessage("Erreur de conversion des données du végétal " + nom_vegetal + "\n");
                return;
            }

            // Choix position du cercle
            PromptPointResult location = ed.GetPoint("\nSpécifiez la position du bloc " + nom_vegetal + " à crée: ");
            if (location.Status != PromptStatus.OK)
            {
                ed.WriteMessage("Création du bloc du végétal " + nom_vegetal + " annulée !\n");
                return;
            }

            string short_type_vegetal;
            /*switch (type_vegetal)
            {
                case "ARBRE":
                    short_type_vegetal = "ARBR";
                    break;
                case "ARBUSTE":
                    short_type_vegetal = "ARBU";
                    break;

                case "VIVACE":
                    short_type_vegetal = "VIV";
                    break;

                case "GRAMINEE":
                    short_type_vegetal = "GRAM";
                    break;

                case "GRIMPANTE":
                    short_type_vegetal = "GRIMP";
                    break;
            }*/

            if (type_vegetal.Length >= 4)
            {
                short_type_vegetal = type_vegetal.Substring(0, 4).ToUpperInvariant();
            }
            else
            {
                short_type_vegetal = type_vegetal.ToUpperInvariant(); 
            }
            ed.WriteMessage("Creation du bloc");
            string bloc_name = "_APUd_VEG_" + short_type_vegetal + "_" + nom_vegetal;
            Color LayerGeneratedColor;
            LayerGeneratedColor = CreateLayer(bloc_name, rgbstring);
            if (hauteur_vegetal != 0)
            {
                CreateLayer("-APUd_VEG_HAUTEURS", "0,0,0", LineWeight.LineWeight120, false);
            }


            //Dessin des cercles periphérique
            void Periph_Circle(double x, double y)
            {
                Point3d cerle_periph_position = new Point3d(0 + (x * largeur_vegetal), 0 + (y * largeur_vegetal), 0);
                DrawCircle(largeur_vegetal, "0", cerle_periph_position);
            }
            Periph_Circle(0, 0);
            Periph_Circle(0.084, -0.05);
            Periph_Circle(0.015, -0.1);
            Periph_Circle(-0.1, 0.065);
            Periph_Circle(-0.09, -0.06);
            Periph_Circle(0.045, 0.05);


            var mtext = new MText();
            string final_text_veg = nom_vegetal;

            if (nom_vegetal.Split(' ').Length >= 0)
            {
                if (nom_vegetal.Split(' ').Length > 0)
                {
                    try
                    {
                        final_text_veg = nom_vegetal.Split(' ')[0];
                }catch (System.Exception) { }
            }
                if (nom_vegetal.Split(' ').Length > 1)
                {
                    try
                    {
                        final_text_veg = final_text_veg + " " + nom_vegetal.Split(' ')[1][0];
                }catch (System.Exception) { }
            }

                if (nom_vegetal.Split(' ').Length > 2)
                {
                    try
                    {
                        final_text_veg = final_text_veg + " " + nom_vegetal.Split(' ')[2];
                    }
                    catch (System.Exception) { }
                    if (nom_vegetal.Split(' ').Length > 3)
                    {
                        try { 
                            if (nom_vegetal.Split(' ')[3][0] != '\'') { 
                        final_text_veg = final_text_veg + " " + nom_vegetal.Split(' ')[3][0];
                            }
                        }
                        catch (System.Exception) { }
                    }
                    if (!(final_text_veg.Substring(final_text_veg.Length - 1) == "\'" || final_text_veg.Substring(final_text_veg.Length - 1) == "'" || final_text_veg.Substring(final_text_veg.Length - 1) == "`"))
                    {
                        if (final_text_veg.Contains('\''))
                        {
                            final_text_veg += "\'";
                        }
                        
                    }
                }
            }

       




            mtext.Contents = final_text_veg;
            mtext.Layer = "0";

            mtext.Location = new Point3d(0, 0, 0);
            mtext.Attachment = AttachmentPoint.MiddleCenter;
            mtext.Width = largeur_vegetal;
            mtext.TextHeight = largeur_vegetal / 5;

            //if (red*0.299 + green*0.587 + blue*0.114) > 186 use #000000 else use #ffffff
            double contrast_background = (299 * LayerGeneratedColor.Red + 587 * LayerGeneratedColor.Green + 114 * LayerGeneratedColor.Blue) / 1000;
            if (contrast_background > 160)
            {
                mtext.Color = Color.FromRgb(0, 0, 0);
            }
            else
            {
                mtext.Color = Color.FromRgb(255, 255, 255);
            }

            mtext.Transparency = new Transparency((byte)255);



            
            ents.Add(mtext);





            if (hauteur_vegetal != 0 && CheckIfLayerExist("-APUd_VEG_HAUTEURS"))
            {

                //var ppr = ed.GetPoint("\nSpécifiez le centre: ");
                Point3d cerle_periph_position = new Point3d(0, 0, 0);


                // using (var tr = db.TransactionManager.StartTransaction())
                // {
                //var curSpace =(BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                var circle = new Circle(cerle_periph_position, Vector3d.ZAxis, largeur_vegetal);

                //circle.TransformBy(ed.CurrentUserCoordinateSystem);
                circle.Layer = "-APUd_VEG_HAUTEURS";

                double Red;
                double Green;
                double Blue;

                if (hauteur_vegetal <= 0.5)
                {
                    //Red to Yellow
                    Red = 255;
                    Green = Math.Floor((double)255 * (hauteur_vegetal - 0) / 1);
                    Blue = 0;

                }
                else if (hauteur_vegetal <= 1)
                {
                    //Yellow to Lime
                    Red = 255 - Math.Floor((double)255 * (hauteur_vegetal - 0.5) / 1);
                    Green = 255;
                    Blue = 0;

                }
                else if (hauteur_vegetal <= 1.5)
                {
                    //Green to Cyan
                    Red = 0;
                    Green = 255;
                    Blue = Math.Floor((double)255 * (hauteur_vegetal - 1) / 1);

                }
                else if (hauteur_vegetal <= 2.5)
                {
                    //Cyan to Blue
                    Red = 0;
                    Green = 255 - Math.Floor((double)255 * (hauteur_vegetal - 1.5) / 1);
                    Blue = 255;

                }
                else
                {
                    //Green to Cyan
                    //Cyan to Blue
                    Red = 0;
                    Green = 0;
                    Blue = 255 - Math.Floor((double)255 * (hauteur_vegetal - 2.5) / 1);
                    if (Blue < 0) { Blue = 0; }
                }

                circle.Color = Color.FromRgb((byte)Red, (byte)Green, (byte)Blue);
                circle.LineWeight = LineWeight.ByLayer;
                circle.Transparency = new Transparency((byte)255);
                // curSpace.AppendEntity(circle);
                //tr.AddNewlyCreatedDBObject(circle, true);

                //   tr.Commit();
                ents.Add(circle);



                MText mText_hauteur = new MText
                {
                    Contents = hauteur_vegetal.ToString(),
                    Layer = "-APUd_VEG_HAUTEURS",

                    Location = new Point3d(0, 0 - largeur_vegetal * 0.7, 0),
                    Attachment = AttachmentPoint.MiddleCenter,
                    Width = largeur_vegetal,
                    TextHeight = largeur_vegetal / 10,
                    Transparency = new Transparency((byte)255),
                    Color = Color.FromRgb((byte)Red, (byte)Green, (byte)Blue)
                    
                };
                ents.Add(mText_hauteur);


            }

       









            if (create_legende)
            {
                ed.WriteMessage("Creation de la legende");
                using (var tr = db.TransactionManager.StartTransaction())
                {


                    BlockTable acBlkTbl = tr.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                    BlockTableRecord acBlkTblRec = tr.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    Point3d tppoint = Point3DToCurentSCU(location.Value);
                    var mtext2 = new MText
                    {
                        Contents = nom_vegetal,
                        Layer = bloc_name,
                        Attachment = AttachmentPoint.MiddleLeft,
                        Location = new Point3d(tppoint.X + largeur_vegetal * 2, tppoint.Y, 0),
                        TextHeight = 0.15,
                        ColorIndex = 7,
                        Transparency = new Transparency((byte)255)
                };
                    acBlkTblRec.AppendEntity(mtext2);
                    tr.AddNewlyCreatedDBObject(mtext2, true);

                    tr.Commit();
                }
            }


            CreateBlock(bloc_name, location.Value);





            // }end file dialog ok
            //}//end using filedialog




        }


        Point3d Point3DToCurentSCU(Point3d point)
        {
            Autodesk.AutoCAD.ApplicationServices.Document doc = AcAp.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;
            return point.TransformBy(ed.CurrentUserCoordinateSystem);
        }

        
        public Hatch HatchInside(ObjectId Objet, Transaction acTrans, BlockTableRecord acBlkTblRec, string layer_hatch)
        {
            Autodesk.AutoCAD.ApplicationServices.Document doc = AcAp.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;
            //var db = doc.Database;
            // using (Transaction acTrans = db.TransactionManager.StartTransaction())
            //{

            //BlockTable acBlkTbl = acTrans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
            //BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
            ObjectIdCollection acObjIdColl = new ObjectIdCollection
            {
                Objet
            };

            // Create the hatch object and append it to the block table record
            using (Hatch acHatch = new Hatch())
            {

                acBlkTblRec.AppendEntity(acHatch);
                acTrans.AddNewlyCreatedDBObject(acHatch, true);
                acHatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                acHatch.Associative = true;
                acHatch.Layer = layer_hatch;
                acHatch.ColorIndex = 256;

                double transparence = -1;
                double calc_transparence = (double)(255 * (100 - transparence) / 100);
                //byte alpha = (byte)Math.Floor(calc_transparence);
                
               
                acHatch.Transparency = new Transparency(TransparencyMethod.ByBlock);
                acHatch.AppendLoop(HatchLoopTypes.Outermost, acObjIdColl);
                acHatch.EvaluateHatch(true);

                //ents.Add(acHatch);
                return acHatch;
            }


            // }
        }



       /* private List<string> GetAllLayerNames(Database db)
        {
            var layers = new List<string>();
            using (var tr = db.TransactionManager.StartOpenCloseTransaction())
            {
                var layerTable = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);
                foreach (ObjectId id in layerTable)
                {
                    var layer = (LayerTableRecord)tr.GetObject(id, OpenMode.ForRead);
                    layers.Add(layer.Name);
                }
            }
            return layers;
        }
       */
        public Circle DrawCircle(double radius, string layer, Point3d location)
        {
            var ed = doc.Editor;
            //var ppr = ed.GetPoint("\nSpécifiez le centre: ");
            Point3d ppr = location;

            // using (var tr = db.TransactionManager.StartTransaction())
            // {
            //var curSpace =(BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
            var circle = new Circle(ppr, Vector3d.ZAxis, radius);

            //circle.TransformBy(ed.CurrentUserCoordinateSystem);
            circle.Layer = layer;
            circle.LineWeight = LineWeight.LineWeight000;
            circle.ColorIndex = 7;
            circle.Transparency = new Transparency((byte)255);
            // curSpace.AppendEntity(circle);
            //tr.AddNewlyCreatedDBObject(circle, true);

            //   tr.Commit();
            ents.Add(circle);
            return circle;
            //  }

        }

        Color CreateLayer(string layername, string rgbstring, LineWeight LineWeight = LineWeight.ByLineWeightDefault, bool print = true)
        {
           // Dictionary<Char, int> RGB_FinalColor = new Dictionary<char, int>() { { 'R', 0 }, { 'G', 0 }, { 'B', 0 } };
            Color couleur;
            Color getRandomColor()
            {
                Random r = new Random();
                return Color.FromRgb((byte)r.Next(20, 220), (byte)r.Next(50, 198), (byte)r.Next(20, 220));
            }
            if (!String.IsNullOrEmpty(rgbstring))
            {
                String[] strlist = rgbstring.Split(',');
                if (strlist.Length == 3)
                {
                    int R = Convert.ToInt32(strlist[0]);
                    int G = Convert.ToInt32(strlist[1]);
                    int B = Convert.ToInt32(strlist[2]);
                    if (R >= 0 && G >= 0 && B >= 0 && R <= 255 && G <= 255 && B <= 255)
                    {
                        couleur = Color.FromRgb((byte)R, (byte)G, (byte)B);
                    }
                    else
                    {
                        couleur = getRandomColor();
                    }
                }
                else
                {
                    couleur = getRandomColor();
                }
            }
            else
            {
                couleur = getRandomColor();
            }
           
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction acTrans = doc.TransactionManager.StartTransaction())
            {
                // Open the Layer table for read
                LayerTable acLyrTbl;
                acLyrTbl = acTrans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                if (acLyrTbl.Has(layername) == false)
                {
                    using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                    {
                        // Assign the layer the ACI color 3 and a name
                        Random r = new Random();

                        // acLyrTblRec.Color = Color.FromRgb((byte)r.Next(20, 200), (byte)r.Next(50, 178), (byte)r.Next(20, 200));
                        acLyrTblRec.Color = couleur;
                        acLyrTblRec.Name = layername;
                        acLyrTblRec.IsPlottable = print;
                        acLyrTblRec.LineWeight = LineWeight;
                        // Upgrade the Layer table for write
                        acLyrTbl.UpgradeOpen();

                        // Append the new layer to the Layer table and the transaction
                        acLyrTbl.Add(acLyrTblRec);
                        acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                    }
                }
                acTrans.Commit();
            }
            return couleur;
        }

        public void CreateBlock(string bloc_name, Point3d location)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            Transaction tr = db.TransactionManager.StartTransaction();
            using (tr)
            {
                // Get the block table from the drawing
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                // Check the block name, to see whether it's
                // already in use
                //PromptStringOptions pso = new PromptStringOptions("\nEnter new block name: ");
                //pso.AllowSpaces = true;
                // A variable for the block's name
                string blkName = bloc_name;

                //PromptResult pr = ed.GetString(pso);
                // Just return if the user cancelled
                // (will abort the transaction as we drop out of the using
                // statement's scope)
                //if (pr.Status != PromptStatus.OK)
                // return;
                try
                {
                    // Validate the provided symbol table name
                    SymbolUtilityServices.ValidateSymbolName(bloc_name, false);
                    // Only set the block name if it isn't in use

                }
                catch
                {
                    // An exception has been thrown, indicating the
                    // name is invalid
                    ed.WriteMessage("\nNom de bloc invalide.");
                }

                if (!bt.Has(bloc_name))
                {





                    // Create our new block table record...
                    BlockTableRecord btr = new BlockTableRecord
                    {
                        // ... and set its properties
                        Name = blkName,
                        Origin = new Point3d(0, 0, 0)
                    };
                    // Add the new block to the block table
                    bt.UpgradeOpen();
                    ObjectId btrId = bt.Add(btr);
                    tr.AddNewlyCreatedDBObject(btr, true);
                    // Add some lines to the block to form a square
                    // (the entities belong directly to the block)

                    foreach (Entity ent in ents)
                    {

                        btr.AppendEntity(ent);
                        tr.AddNewlyCreatedDBObject(ent, true);
                        if (ent.ObjectId == ents[0].ObjectId)
                        {
                            Hatch hatch_interrieur = HatchInside(ents[0].ObjectId, tr, btr,"0");
                            //Hatch hatch_interrieur_color = HatchInside(ents[0].ObjectId, tr, btr);
                        }
                    }


                    //Hatch hatch_interrieur = HatchInside(ents[0].ObjectId, tr, btr);
                    // Add a block reference to the model space
                    BlockTableRecord ms = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    BlockReference br = new BlockReference(Point3DToCurentSCU(location), btrId)
                    {
                        Layer = bloc_name
                    };
                    br.Transparency = new Transparency(TransparencyMethod.ByLayer);
                    ms.AppendEntity(br);
                    tr.AddNewlyCreatedDBObject(br, true);
                }
                else
                {
                    ed.WriteMessage("\nLe bloc \"" + bloc_name + "\" existe déja.");
                    using (var br = new BlockReference(Point3DToCurentSCU(location), bt[bloc_name]))
                    {
                        br.Layer = bloc_name;
                        br.Transparency = new Transparency(TransparencyMethod.ByLayer);
                        var space = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                        space.AppendEntity(br);
                        tr.AddNewlyCreatedDBObject(br, true);
                    }
                }
                // Commit the transaction
                tr.Commit();
                // Report what we've done
                ents.Clear();
                ed.WriteMessage("\nBloc crée avec succès !");
            }
        }



        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(str)) { return null; }
            string strrepl = str.Trim();
                strrepl = strrepl.Replace('`','\'');
            strrepl = strrepl.Replace('"','\'');
            strrepl = strrepl.Replace('é','e');
            strrepl = strrepl.Replace('ç','c');
            strrepl = strrepl.Replace('\\','+');
            foreach (char c in strrepl)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == '-' || c == ' ' || c == '\'' || c=='`' || c=='+')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }


        public bool CheckIfLayerExist(string layername)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction acTrans = doc.TransactionManager.StartTransaction())
            {
                // Open the Layer table for read
                LayerTable acLyrTbl;
                acLyrTbl = acTrans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                if (acLyrTbl.Has(layername))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }

}
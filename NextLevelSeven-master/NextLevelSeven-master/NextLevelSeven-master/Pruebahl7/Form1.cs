using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NextLevelSeven;
using NextLevelSeven.Core;

namespace Pruebahl7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string peticion = @"MSH|^~\&|SIAP|MINSAL|IOLIS|TECNODIAGNOSTICA - VITEK 2 Compact|201705021121||OML^O21|1|D|2.5.1|||AL|AL|||||
PID|1||911-16^^^30||JAIME AVILES^CESAR^EDUARDO ||201705031121|1
PV1|1|2|MINSAL-Hospitalización
ORC|NW|112||1|||||201705021200|||716^CERON RIVERA^ADA NOHEMY^|55^^^^^^^^Cirugía Hombres 1||||1^Ministerio de Salud||||Hospital Nacional Santa Tecla LI San Rafael^^30
OBR|1|1069||298^HEMOCULTIVO^^M19|||201705021112||||||||1069
SPM|1|1069||1^Sangre||||^^|||||||||201705021212^";
            var mensaje = NextLevelSeven.Core.Message.Build(peticion);
            Console.WriteLine(peticion);

            var segmentos = mensaje[1];
            // first segment in a message (returns IElement)
            var mshSegment = mensaje[1];

   

     

            // 1st segment, 9th field, 1st repetition, 2nd component (returns IElement)
            var messageTriggerEvent = mensaje[1][9][1][2];

            // 1st segment, 9th field, 2nd component (returns IComponent)
            // note: the 1st repetition is implied in this format unless specified
            var  messageTriggerEvent2 = mensaje.Segment(2).Field(4).Component(1);

            // get the first PID segment
            var pidSegment = mensaje.Segments.OfType("PID").First();
            var pid1segment = pidSegment.Field(1).Component(1);
            var pid31segment = pidSegment.Field(3).Component(1);
            var pid32segment = pidSegment.Field(3).Component(2);
            var pid51segment = pidSegment.Field(5).Component(1);
            var pid52segment = pidSegment.Field(5).Component(2);
            var pid53segment = pidSegment.Field(5).Component(3);
            var pid7segment = pidSegment.Field(7).Component(1);
            var pid8egment = pidSegment.Field(8).Component(1);

            var pv1Segment = mensaje.Segments.OfType("PV1").First();
            var orcSegment = mensaje.Segments.OfType("ORC").First();
            var obrSegment = mensaje.Segments.OfType("OBR").First();
            var SpmSegment = mensaje.Segments.OfType("SPM").First();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

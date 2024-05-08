using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestTrump
{
    /// <summary>
    /// Resultado de controles tipo Dialogo POPUP
    /// </summary>
    public enum enuDlgPopupItemSelected
    {
        None,
        //Precios
        PrecioMaximo, PrecioMayor, PrecioMinimo, PrecioManual, PrecioOferta,
        //CRUD
        CRUD_Nuevo, CRUD_Modificar, CRUD_Consultar, CRUD_Eliminar, CRUD_Reimprimir,
        //Ingresos Egresos en CAJA
        MovCaja_RetiroCaja, MovCaja_PagoProveedores, MovCaja_DepositoBancario, MovCaja_ValePersonal, MovCaja_Otro,
        MovCaja_IngresoEfectivo, MovCaja_AbonoCliente, MovCaja_NotaCredito, MovCaja_TransaccionBancaria,
        //Abonos
        Abono_Cliente, Abono_Proveedor, Abono_Otro,
        //Tipos Pago
        Pago_Efectivo, Pago_TD, Pago_TC, Pago_PagoMixto
    }
    public partial class ctrlBase : UserControl
    {
        private enuDlgPopupItemSelected popupresult;

        public ctrlBase()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("Personalizado")]
        [DefaultValue("Dialogo Modal")]
        public string Titulo
        {
            get { return lblTitulo.Text; }
            set { lblTitulo.Text = value; }
        }

        public enuDlgPopupItemSelected enuDlgPopupItemResult
        {
            get => popupresult;
            set
            {
                popupresult = value;
                if (popupresult != enuDlgPopupItemSelected.None)
                {
                    CloseCtrl();
                }
            }
        }
        public CloseReason CloseCtrl(CloseReason RazonCierre = CloseReason.UserClosing)
        {
            //switch (RazonCierre)
            //{
            //    case CloseReason.None:
            //        cerrar = false;
            //        break;
            //    case CloseReason.UserClosing:
            //    case CloseReason.ApplicationExitCall:
            //    case CloseReason.FormOwnerClosing:
            //    case CloseReason.MdiFormClosing:
            //    case CloseReason.TaskManagerClosing:
            //    case CloseReason.WindowsShutDown:
            //        cerrar = true;
            //        break;
            //}
            this.Dispose();
            return RazonCierre;
        }
        private void icoMsj_Click(object sender, EventArgs e)
        {
            CloseCtrl();
        }

        private void ctrlBase_Load(object sender, EventArgs e)
        {
            enuDlgPopupItemResult = enuDlgPopupItemSelected.None;
        }

        private void ctrlBase_VisibleChanged(object sender, EventArgs e)
        {
            //enuDlgPopupItemResult = enuDlgPopupItemSelected.None;
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using static ksslib_c.Utiles.clsUtil_Perifericos;

namespace RestTrump
{
    public delegate void ctrlClosedEventHandler(object sender);

    public enum enuTipoCtrlDialog
    {
        BackTransparent,
        Contenido,
        Popup,
        Float
    }
    public class clsCargarDialogControl
    {
        private readonly Form mForm;
        private static monitorMouseTeclado monMouseTeclado = null;
        private bool esTieneFoco;
        private bool esRunning;
        private bool esModal;
        private Control ctrlactual;
        public string IconoSymbolMsj { get; private set; }
        public Image ImageMsj { get; private set; }
        public string Mensaje { get; private set; }
        public string TituloMsj { get; private set; }
        public ctrlTransparent ctrlTransparent { get; set; }
        ////public ctrlsMsj ctrlsMsj { get; set; }
        //public enuResponseOpcion BotonesRespuesta { get; private set; }
        //public enuBtnMsjRespuesta MsjResult { get; private set; }
        public enuDlgPopupItemSelected DlgPopupResult;
        private string Personalizado1;
        private string Personalizado2;
        private bool esLocal;

        /// <summary>
        /// COntrol contenido en la carga
        /// </summary>
        private Control ctrlActual
        {
            get { return ctrlactual; }
            set
            {
                ctrlactual = value;
                if (ctrlactual != null)
                {
                    ctrlActual.Visible = false;
                    ctrlActual.Paint += CtrlActual_Paint;
                    ctrlActual.Disposed += CtrlActual_Disposed;
                    ctrlactual.VisibleChanged += Ctrlactual_VisibleChanged;
                }
            }
        }

        private void Ctrlactual_VisibleChanged(object sender, EventArgs e)
        {
            if (ctrlactual != null)
                if (!ctrlActual.Visible)
                    Close();
        }

        public enuTipoCtrlDialog TipoDialogo { get; set; }

        /// <summary>
        /// Iniciar Objeto
        /// </summary>
        /// <param name="form"></param>
        public clsCargarDialogControl(Form form)
        {
            esTieneFoco = false;
            esRunning = false;
            mForm = form;
            if (ctrlTransparent == null)
                ctrlTransparent = new ctrlTransparent(mForm);

            if (monMouseTeclado == null)
            {
                monMouseTeclado = new monitorMouseTeclado();
                Application.AddMessageFilter(monMouseTeclado);
            }
            Cursor.Position = new Point(ctrlTransparent.Width / 2, ctrlTransparent.Height / 2);
        }

        public event ctrlClosedEventHandler DialogClosed;
        private void onDialogClosed(object sender)
        {
            DialogClosed?.Invoke(sender);
        }
        private void mf_MouseDown()
        {
            if (ctrlActual != null)
                if (ctrlActual.Visible)
                {
                    if (!ctrlActual.RectangleToScreen(ctrlActual.ClientRectangle).Contains(Cursor.Position))
                    {
                        switch (TipoDialogo)
                        {
                            case enuTipoCtrlDialog.BackTransparent:
                            case enuTipoCtrlDialog.Popup:
                                Close(CloseReason.None);
                                break;
                            case enuTipoCtrlDialog.Float:
                                MinimizarDlg();
                                break;
                            default:
                                break;
                        }
                        esTieneFoco = false;
                    }
                    else
                    {
                        esTieneFoco = true;
                        ctrlActual.Visible = true;
                    }
                }
        }
        private void mf_KeyUp(IntPtr target)
        {
            if (!target.Equals(IntPtr.Zero))
            {
                if (ctrlActual != null)
                    if (ctrlActual.Visible)
                    {

                    }
            }
        }
        private void MaximizarDlg()
        {
            ctrlBase ctrlFloat = ctrlActual as ctrlBase;
            //if (ctrlFloat != null)
            //    ctrlFloat.Maximizar();
        }
        private void MinimizarDlg()
        {
            ctrlBase ctrlFloat = ctrlActual as ctrlBase;
            //if (ctrlFloat != null)
            //    ctrlFloat.Minimizar();
        }
        public void Close(CloseReason RazonCierre = CloseReason.UserClosing)
        {
            esRunning = false;
            //if (Completed != null) Completed(this, EventArgs.Empty);
            switch (TipoDialogo)
            {
                case enuTipoCtrlDialog.BackTransparent:
                    bool cerrar = false;
                    if (!esModal)
                        cerrar = true;
                    else
                    {
                        switch (RazonCierre)
                        {
                            case CloseReason.None:
                                cerrar = false;
                                break;
                            case CloseReason.UserClosing:
                            case CloseReason.ApplicationExitCall:
                            case CloseReason.FormOwnerClosing:
                            case CloseReason.MdiFormClosing:
                            case CloseReason.TaskManagerClosing:
                            case CloseReason.WindowsShutDown:
                                cerrar = true;
                                break;
                        }
                    }
                    if (cerrar)
                    {
                        monMouseTeclado.MouseDown -= new monitorMouseTeclado.LeftButtonDown(mf_MouseDown);
                        monMouseTeclado.KeyUp -= new monitorMouseTeclado.KeyPressUp(mf_KeyUp);
                        ctrlBase popupResult = ctrlActual as ctrlBase;
                        if (popupResult != null)
                            DlgPopupResult = popupResult.enuDlgPopupItemResult;
                        if (ctrlActual != null)
                        {
                            if (ctrlActual.Parent != null)
                                ctrlActual.Parent.Visible = false;
                            ctrlactual.Parent = null;
                            if (!esLocal)
                                ctrlActual.Dispose();
                            ctrlTransparent.SendToBack();
                            ctrlTransparent.Refresh();
                            //if (ctrlTransparent != null)
                            //    ctrlTransparent = null;
                        }
                    }
                    else
                        Blinkear();

                    break;
                case enuTipoCtrlDialog.Contenido:
                    //var convertedObject = Convert.ChangeType(Contenedor, Contenedor.GetType());
                    //if (Contenedor != null)
                    //{
                    //    dynamic convertedObject = Contenedor;
                    //    convertedObject.Visible = false;
                    //}
                    ctrlActual.Visible = false;
                    break;
                case enuTipoCtrlDialog.Popup:
                    ctrlBase dlgpopupResult = ctrlActual as ctrlBase;
                    if (dlgpopupResult != null)
                        DlgPopupResult = dlgpopupResult.enuDlgPopupItemResult;
                    ctrlActual.Visible = false;
                    break;
            }
        }
        private void centrarControl(Control control)
        {
            control.Left = control.Parent.Width / 2 - control.Width / 2;
            control.Top = control.Parent.Height / 2 - control.Height / 2;
        }
        private void Blinkear()
        {
            if (ctrlActual != null)
            {
                //ctrl.BorderStyle = BorderStyle.FixedSingle;
                Color tmpColor = ctrlActual.BackColor;
                ctrlActual.BackColor = Color.Red;
                System.Media.SystemSounds.Beep.Play();
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
                ctrlActual.BackColor = tmpColor;
                //this.BorderStyle = BorderStyle.None;
            }
        }

        #region Mostrar Dialogo
        public void cargarDlgBackTransparent(Control Dialogo, bool Modal = false, bool eslocal = false)
        {
            ctrlActual = Dialogo;
            TipoDialogo = enuTipoCtrlDialog.BackTransparent;
            esModal = Modal;
            monMouseTeclado.MouseDown += new monitorMouseTeclado.LeftButtonDown(mf_MouseDown);
            monMouseTeclado.KeyUp += new monitorMouseTeclado.KeyPressUp(mf_KeyUp);
            esLocal = eslocal;
            MostrarDialogo();
        }

        internal void cargarDlgFloat(Control Dialogo)
        {
            ctrlActual = Dialogo;
            TipoDialogo = enuTipoCtrlDialog.Float;
            esModal = false;
            MostrarDialogo();
            monMouseTeclado.MouseDown += new monitorMouseTeclado.LeftButtonDown(mf_MouseDown);
            monMouseTeclado.KeyUp += new monitorMouseTeclado.KeyPressUp(mf_KeyUp);
        }

        private void CtrlActual_Disposed(object sender, EventArgs e)
        {
            try
            {
                onDialogClosed(ctrlActual);
                esRunning = false;
                Close(CloseReason.UserClosing);
                ctrlActual = null;
            }
            finally
            {
                ctrlTransparent.Visible = false;
            }
        }

        private void CtrlActual_Paint(object sender, PaintEventArgs e)
        {
            esRunning = true;
        }

        private void MostrarDialogo()
        {
            Cursor tmpCursor = mForm.Cursor;
            mForm.Cursor = Cursors.WaitCursor;

            switch (TipoDialogo)
            {
                case enuTipoCtrlDialog.BackTransparent:
                    ctrlTransparent.MinimumSize = mForm.ClientSize;
                    ctrlTransparent.Controls.Clear();
                    ctrlTransparent.Controls.Add(ctrlActual);
                    ctrlTransparent.BringToFront();
                    ctrlTransparent.Visible = true;
                    //while (!ctrlTransparent.isPainted)
                    //    Application.DoEvents();
                    break;
                case enuTipoCtrlDialog.Popup:
                    break;
                case enuTipoCtrlDialog.Float:
                    if (mForm.Controls.Contains(ctrlActual))
                        MaximizarDlg();
                    else
                        mForm.Controls.Add(ctrlActual);
                    break;
                default:
                    break;
            }
            centrarControl(ctrlActual);
            ctrlActual.Visible = true;
            ctrlActual.BringToFront();
            Cursor.Position = new Point(ctrlActual.Left + (ctrlActual.Width / 2), ctrlActual.Top + 10);
            mForm.Cursor = tmpCursor;

        }


        #endregion

        //#region Mostrar Mensaje
        //public void MostrarMsj(string mensaje, string tituloMsj, Image imageMsj, string iconoSymbolMsj)
        //{
        //    Mensaje = mensaje;
        //    TituloMsj = tituloMsj;
        //    BotonesRespuesta = enuResponseOpcion.Ninguno;
        //    ImageMsj = imageMsj;
        //    IconoSymbolMsj = iconoSymbolMsj;
        //    MostrarMsj();
        //}
        //public void MostrarMsj(string mensaje, string tituloMsj, enuResponseOpcion botonesRespuesta, string personalizado1 = "", string personalizado2 = "")
        //{
        //    Mensaje = mensaje;
        //    TituloMsj = tituloMsj;
        //    BotonesRespuesta = botonesRespuesta;
        //    Personalizado1 = personalizado1;
        //    Personalizado2 = personalizado2;
        //    MostrarMsj();
        //}
        //private void MostrarMsj()
        //{
        //    if (ctrlTransparent != null)
        //    {
        //        if (ctrlsMsj == null)
        //        {
        //            ctrlsMsj = new RestTrump.ctrlsMsj();
        //            ctrlsMsj.Completed += CtrlsMsj_Completed;
        //        }
        //        ctrlsMsj.lblTituloMsj.Text = TituloMsj;
        //        ctrlsMsj.lblMsj.Text = Mensaje;
        //        ctrlsMsj.Size = ctrlsMsj.ctrlMedidaStandard;

        //        string icoMsj = "";
        //        if (IconoSymbolMsj != null)
        //            icoMsj = ctrlsMsj.icoMsj.Symbol = IconoSymbolMsj;

        //        Image imsj = null;
        //        if (ImageMsj != null)
        //            imsj = ctrlsMsj.picMsj.Image = ImageMsj;

        //        Control[] btns = ctrlsMsj. btns;
        //        ctrlsMsj.btnsResponse(ref icoMsj, ref imsj, ref btns, BotonesRespuesta, Personalizado1, Personalizado2);

        //        ctrlsMsj.picMsj.Visible = imsj == null ? false : true;
        //        if (ctrlsMsj.picMsj.Visible)
        //            ctrlsMsj.picMsj.Image = imsj;

        //        ctrlsMsj.lblTituloMsj.Visible = ctrlsMsj.lblTituloMsj.Text.Trim().Length == 0 ? false : true;
        //        ctrlsMsj.icoMsj.Visible = icoMsj.Trim().Length == 0 ? false : true;
        //        if (ctrlsMsj.icoMsj.Visible)
        //            ctrlsMsj.icoMsj.Symbol = icoMsj;

        //        ctrlActual = ctrlsMsj;
        //        MostrarDialogo();
        //        while (ctrlsMsj.isRunning)
        //            Application.DoEvents();
        //        MsjResult = ctrlsMsj.Result;
        //    }
        //}
        //private void CtrlsMsj_Completed(object sender, EventArgs e)
        //{
        //    if (ctrlTransparent != null) ctrlTransparent.Visible = false;
        //    MsjResult = ctrlsMsj.Result;
        //}
        //#endregion




    }

}
////To show a MessageBox, the syntax is as following:

//CustomMessageBox b = new CustomMessageBox("hello world");
//        c.DialogClosed += ()=>
//{
//   // the rest of the function
//}
//// this raises an event listened for by the main window view model,
//// displaying the message box and greying out the rest of the program.
//base.ShowMessageBox(b);
using System;
using System.Windows.Forms;
using ksslib_c;
using ksslib_c.Utiles;

namespace vPOS
{
   internal  class cls_configAppLITE : cls_configApp
	{
		#region "Version LITE"
		//Aplicacion LITE  restringir limite de registros de operaciones,
		const int arcServicios_Max = 20;
		const int arcClientes_Max = 100;
		const int arcUsuarios_Max = 1;
		const int arcFormasPago_Max = 2;
		const int arcDescuentos_Max = 2;

        public bool esPosponerReset { get; internal set; }
        public string TimeAutofixPosponer { get; internal set; }

        #endregion
        #region "Metodos verificar y modificar BD LITE"

        /// <summary>
        /// Buscar y borrar datos extras según restricciones de licencia.
        /// </summary>
        public void borrarExcesosBDLITE()
		{
			//Leer la tablas y eliminar registros sobrantes
			switch (base.DatosLicencia.enuTipoLicencia)
			{
				case  enuTipoLicencia.Demo:
				case   enuTipoLicencia.Estudiante:
				case enuTipoLicencia.Free:
				case enuTipoLicencia.ShareWare:
				case enuTipoLicencia.Trial:
					//Enlazar a datos
					//Tabla por tabla
					break;
			}

		}

		/// <summary>
		/// Metodo que verifica cada nuevo registro y las limitaciones de la DatosLicencia.
		/// </summary>
		/// <param name="mBindingSource">BindingSource Enlazado a Datos</param>
		/// <param name="RowsActuales">Filas Actuales</param>
		/// <param name="NombreTabla">Nombre de Tabla a restringir</param>
		/// <returns>
		/// Valor Booleano
		/// true: Cuando se ejecuta sin errores
		/// </returns>
		public bool verificarNewRowTablaLITE(
			BindingSource mBindingSource,
			int RowsActuales,
			string NombreTabla)
		{
			try
			{
				switch (base.DatosLicencia.enuTipoLicencia)
				{
					case enuTipoLicencia.Demo:
					case enuTipoLicencia.Estudiante:
					case enuTipoLicencia.Free:
					case enuTipoLicencia.ShareWare:
					case enuTipoLicencia.Trial:
						int ValorLimite = 0;
						switch (NombreTabla.ToLower())
						{
							case "clientes":
								ValorLimite = arcClientes_Max;
								break;
							case "usuarios":
								ValorLimite = arcUsuarios_Max;
								break;
							case "tipopago":
								ValorLimite = arcFormasPago_Max;
								break;
							case "descuentos":
								ValorLimite = arcDescuentos_Max;
								break;
							case "servicios":
								ValorLimite = arcServicios_Max;
								break;
						}
						if (RowsActuales == ValorLimite)
						{
							mBindingSource.CancelEdit();
							//Mensaje version LITE
							ksslib.kss_msjDelay.Show(string.Format("Ha alcanzado el valor Máximo ({0}) de registros. /n Adquiera un Versión Completa del Software.", ValorLimite), ksslib.enuMsgBoxImag.msgInformacion);
						}
						else
						{
							mBindingSource.EndEdit();
						}
						break;

				}
				return true;
			}
			catch (Exception ex)
			{
				ksslib.clsUtilErrors.Manejador_errores(ex);
				return false;
			}
		}
		#endregion

		public cls_configAppLITE(bool esIniciandoApp) : base(esIniciandoApp)
		{
		}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado
    {
        private int id_empleado;
        private string dni;
        private string nombres;
        private string apellidos;
        private string ciudad;
        private string direccion;
        private string telefono;
        private double pago_dia;
        private DateTime fecha_contrato;
        private DateTime fecha_fin_contrato;
        private string estado;

        public int Id_empleado { get => id_empleado; set => id_empleado = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public double Pago_dia { get => pago_dia; set => pago_dia = value; }
        public DateTime Fecha_contrato { get => fecha_contrato; set => fecha_contrato = value; }
        public DateTime Fecha_fin_contrato { get => fecha_fin_contrato; set => fecha_fin_contrato = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}

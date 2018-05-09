using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DatosPeticion
/// </summary>
public class DatosPeticion
{

    private int indice;
    private int orden;
    private string fsolicitud;
    private int origin;
    private int servicio;
    private string doctor;
    private int libre;
    private int Identificacion;
    private string nombre;
    private string apellido1;
    private string apellido2;
    private string FNac;
    private string codigo;
    private string sexo;
    private int edad;
    private string hl7Peticion;

    public DatosPeticion()
    {

    }

    public DatosPeticion(int indice, int orden, string fsolicitud, int origin, int servicio, string doctor, int libre, int identificacion, string nombre, string apellido1, string apellido2, string fNac, string codigo, string sexo, int edad)
    {
        this.indice = indice;
        this.orden = orden;
        this.fsolicitud = fsolicitud;
        this.origin = origin;
        this.servicio = servicio;
        this.doctor = doctor;
        this.libre = libre;
        Identificacion = identificacion;
        this.nombre = nombre;
        this.apellido1 = apellido1;
        this.apellido2 = apellido2;
        FNac = fNac;
        this.codigo = codigo;
        this.sexo = sexo;
        this.edad = edad;
    }

    public int Indice
    {
        get
        {
            return indice;
        }

        set
        {
            indice = value;
        }
    }

    public int Orden
    {
        get
        {
            return orden;
        }

        set
        {
            orden = value;
        }
    }

    public string Fsolicitud
    {
        get
        {
            return fsolicitud;
        }

        set
        {
            fsolicitud = value;
        }
    }

    public int Origin
    {
        get
        {
            return origin;
        }

        set
        {
            origin = value;
        }
    }

    public int Servicio
    {
        get
        {
            return servicio;
        }

        set
        {
            servicio = value;
        }
    }

    public string Doctor
    {
        get
        {
            return doctor;
        }

        set
        {
            doctor = value;
        }
    }

    public int Libre
    {
        get
        {
            return libre;
        }

        set
        {
            libre = value;
        }
    }

    public int Identificacion1
    {
        get
        {
            return Identificacion;
        }

        set
        {
            Identificacion = value;
        }
    }

    public string Nombre
    {
        get
        {
            return nombre;
        }

        set
        {
            nombre = value;
        }
    }

    public string Apellido1
    {
        get
        {
            return apellido1;
        }

        set
        {
            apellido1 = value;
        }
    }

    public string Apellido2
    {
        get
        {
            return apellido2;
        }

        set
        {
            apellido2 = value;
        }
    }

    public string FNac1
    {
        get
        {
            return FNac;
        }

        set
        {
            FNac = value;
        }
    }

    public string Codigo
    {
        get
        {
            return codigo;
        }

        set
        {
            codigo = value;
        }
    }

    public int Edad
    {
        get
        {
            return edad;
        }

        set
        {
            edad = value;
        }
    }

    public int Indice1
    {
        get
        {
            return indice;
        }

        set
        {
            indice = value;
        }
    }

    public int Orden1
    {
        get
        {
            return orden;
        }

        set
        {
            orden = value;
        }
    }

    public string Fsolicitud1
    {
        get
        {
            return fsolicitud;
        }

        set
        {
            fsolicitud = value;
        }
    }

    public int Origin1
    {
        get
        {
            return origin;
        }

        set
        {
            origin = value;
        }
    }

    public int Servicio1
    {
        get
        {
            return servicio;
        }

        set
        {
            servicio = value;
        }
    }

    public string Doctor1
    {
        get
        {
            return doctor;
        }

        set
        {
            doctor = value;
        }
    }

    public int Libre1
    {
        get
        {
            return libre;
        }

        set
        {
            libre = value;
        }
    }

    public int Identificacion2
    {
        get
        {
            return Identificacion;
        }

        set
        {
            Identificacion = value;
        }
    }

    public string Nombre1
    {
        get
        {
            return nombre;
        }

        set
        {
            nombre = value;
        }
    }

    public string Apellido11
    {
        get
        {
            return apellido1;
        }

        set
        {
            apellido1 = value;
        }
    }

    public string Apellido21
    {
        get
        {
            return apellido2;
        }

        set
        {
            apellido2 = value;
        }
    }

    public string FNac2
    {
        get
        {
            return FNac;
        }

        set
        {
            FNac = value;
        }
    }

    public string Codigo1
    {
        get
        {
            return codigo;
        }

        set
        {
            codigo = value;
        }
    }

    public string Sexo
    {
        get
        {
            return sexo;
        }

        set
        {
            sexo = value;
        }
    }

    public int Edad1
    {
        get
        {
            return edad;
        }

        set
        {
            edad = value;
        }
    }

    public string Hl7Peticion
    {
        get
        {
            return hl7Peticion;
        }

        set
        {
            hl7Peticion = value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DatosPeticion
/// </summary>
public class DatosPeticion
{

    private long indice;
    private long orden;
    private string fsolicitud;
    private long origin;
    private long servicio;
    private string doctor;
    private long libre;
    private long Identificacion;
    private string nombre;
    private string apellido1;
    private string apellido2;
    private string FNac;
    private string codigo;
    private string sexo;
    private long edad;
    private string hl7Peticion;

    public DatosPeticion()
    {

    }

    public DatosPeticion(long indice, long orden, string fsolicitud, long origin, long servicio, string doctor, long libre, long identificacion, string nombre, string apellido1, string apellido2, string fNac, string codigo, string sexo, long edad)
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

    public long Indice
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

    public long Orden
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

    public long Origin
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

    public long Servicio
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

    public long Libre
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

    public long Identificacion1
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

    public long Edad
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

    public long Indice1
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

    public long Orden1
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

    public long Origin1
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

    public long Servicio1
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

    public long Libre1
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

    public long Identificacion2
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

    public long Edad1
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
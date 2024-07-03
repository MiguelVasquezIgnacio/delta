using System.ComponentModel.DataAnnotations;

namespace MiRadio_Startup.Dtos
{
    public enum RolEnum
    {
        Administrador=1,
        Moderador
    }

    public class MiDTO
    {
        [EnumDataType(typeof(RolEnum))]
        public RolEnum Rol { get; set; }
    }
}

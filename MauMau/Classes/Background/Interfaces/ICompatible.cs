using MauMau.Classes.Background.Enum;

namespace MauMau.Classes.Background.Interfaces
{
   public interface ICompatible
    {
        bool Compatible(ICompatible card);
        bool Compatible(ICompatible card, Cor color);
    }
}

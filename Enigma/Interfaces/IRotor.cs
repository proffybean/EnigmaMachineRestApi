namespace Enigma.Interfaces
{
    public interface IRotor
    {
        int Offset { get; set; }
        char ConvertLetter(char c);
        char ConvertLetter(int i);
        void Rotate();
        void SetDial(int i);
        int GetDialOffset();
        char DialSetting { get; }
    }
}

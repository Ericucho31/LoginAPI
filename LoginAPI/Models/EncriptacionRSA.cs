using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class EncriptacionRSA
{
    private static Random random = new Random();
    // Declaracion de variables de RSA
    public static BigInteger p { get; set; }
    public static BigInteger q { get; set; }
    public static BigInteger n { get; set; }
    public static BigInteger phi { get; set; }
    public static BigInteger e { get; set; }
    public static BigInteger d { get; set; }

    public EncriptacionRSA()
    {
        p = 89;
        q = 97;
        n = p * q;
        phi = (p - 1) * (q - 1);
        e = GenerateE(phi);
        d = ModInverse(e, phi);
}
   

    // Función para generar un valor e coprimo con phi
    private static BigInteger GenerateE(BigInteger phi)
    {
        BigInteger e = 3; // Empezar con un valor pequeño para e
        while (BigInteger.GreatestCommonDivisor(e, phi) != 1)
        {
            e += 2; // Incrementar e hasta que sea coprimo con phi
        }
        return e;
    }

    // Función para calcular el inverso multiplicativo modular de e (mod phi)
    private static BigInteger ModInverse(BigInteger e, BigInteger phi)
    {
        BigInteger d = 0, x1 = 0, x2 = 1, y1 = 1, temp_phi = phi;
        while (e > 0)
        {
            BigInteger temp1 = temp_phi / e;
            BigInteger temp2 = temp_phi - temp1 * e;
            temp_phi = e;
            e = temp2;

            BigInteger x = x2 - temp1 * x1;
            BigInteger y = d - temp1 * y1;

            x2 = x1;
            x1 = x;
            d = y1;
            y1 = y;
        }

        if (temp_phi == 1)
            return d + phi;
        return 0;
    }

    public  int[] StringToIntArray(string cadena)
    {
        
        string[] stringArray = cadena.Split(',');
        int[] intArray = new int[stringArray.Length];

        for (int i = 0; i < stringArray.Length; i++)
        {
            intArray[i] = int.Parse(stringArray[i]);
        }
        return intArray;
    }

    public string Encriptacion(char[] mensajeDividido)
    {
        int[] arregloDeNumeros = new int[mensajeDividido.Length];
        int[] arregloEncriptado = new int[mensajeDividido.Length];

        for (int i = 0; i < mensajeDividido.Length; i++)
        {
            arregloDeNumeros[i] = (int)mensajeDividido[i];
            Console.WriteLine("El valor ascii de la letra es : " + arregloDeNumeros[i]);

            BigInteger mensajeCifrado = BigInteger.ModPow(arregloDeNumeros[i], e, n);
            Console.WriteLine("Mensaje cifrado:" + mensajeCifrado);

            arregloEncriptado[i] = (int)mensajeCifrado;
        }
        string numerosString = string.Join(",", arregloEncriptado);
        return numerosString;
    }

    public  string Desencriptacion(string passwordEncriptado)
    {
        int[] arregloEncriptado = StringToIntArray(passwordEncriptado);
        char[] arregloDesencriptado = new char[arregloEncriptado.Length];
        for (int i = 0; i < arregloEncriptado.Length; i++)
        {
            // Descifrado RSA
            BigInteger mensajeDescifrado = BigInteger.ModPow(arregloEncriptado[i], d, n);
            arregloDesencriptado[i]= (char)mensajeDescifrado;
        }
        string mensajeDesencriptado = new string(arregloDesencriptado);
        return mensajeDesencriptado;
    }
}

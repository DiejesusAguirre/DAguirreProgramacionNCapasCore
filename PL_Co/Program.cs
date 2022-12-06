

using System.Text;

ReadFile();
Console.ReadKey();


static void ReadFile()
{
    ML.Result result = new ML.Result();
    ML.Usuario usuario = new ML.Usuario();
    string file = @"C:\Users\digis\Documents\Vega Aguirre Diego Jesus\ArchivosTexto\LayoutUsuario.txt"; 
    string fileError = @"C:\Users\digis\Documents\Vega Aguirre Diego Jesus\ArchivosTexto\ErroresLayoutl.txt";
    StreamWriter streamWriter = new StreamWriter(fileError, true, Encoding.ASCII);

    if (File.Exists(file))
    {

        StreamReader Textfile = new StreamReader(file);
            //File.CreateText(f);

        string line;
        line = Textfile.ReadLine();
        while ((line = Textfile.ReadLine()) != null)
        {
            string[] lines = line.Split('|');

            
            usuario.Nombre = lines[0];
            usuario.ApellidoPaterno = lines[1];
            usuario.ApellidoMaterno = lines[2];
            usuario.Sexo = lines[3];
            usuario.Email = lines[4];
            usuario.Password = lines[5];
            usuario.UserName = lines[6];
            usuario.Telefono = lines[7];
            usuario.Celular = lines[8];
            usuario.CURP = lines[9];

            usuario.ROL = new ML.Rol();
            usuario.ROL.IdRol = int.Parse(lines[10]);

            usuario.FechaDeNacimiento = lines[11];
            usuario.Imagen = null;

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Calle = lines[12];
            usuario.Direccion.NumeroInterior = lines[13];
            usuario.Direccion.NumeroExterior = lines[14];

            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);


            result = BL.Usuario.Add(usuario);


            if (result.Correct)
            {
                Console.WriteLine("Correcto");
                Console.ReadKey();
            }
            else
            {
                streamWriter.WriteLine("Tus errores son" + result.ErrorMessage);
            }
        }
        if(result.Correct == true)
        {
            Console.WriteLine("Ejecucion Realizada Exitosa");
            streamWriter.WriteLine("Todo Exitoso");
            streamWriter.Close();
        }
        else
        {
            result = BL.Usuario.Add(usuario);
            Console.WriteLine(result.ErrorMessage);
            streamWriter.WriteLine("Error que fuecausado: " + result.ErrorMessage);
            streamWriter.Close();
        }

    }
    else
    {
        streamWriter.WriteLine("No hay archivo disponible");
        streamWriter.Close();
    }
}

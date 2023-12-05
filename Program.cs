using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

class Tarea4
{
    static void Main()
    {
        try
        {
            var Optionss = new EdgeOptions();
            var driverr = new EdgeDriver(Optionss);

            string evidencia_del_Folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Evidencia");

            //Crear la carpeta si no existe
            if (!Directory.Exists(evidencia_del_Folder))
            {
                Directory.CreateDirectory(evidencia_del_Folder);
            }

            //Link para ir a la plataforma virtual
            driverr.Navigate().GoToUrl("https://plataformavirtual.itla.edu.do/login/index.php");

            //Pausa
            Thread.Sleep(2000);

            //Captura en la página de inicio
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Inicio.png"));

            var BuscarBox = driverr.FindElement(By.Id("username"));
            BuscarBox.SendKeys("20220997");

            var BuscarBox2 = driverr.FindElement(By.Id("password"));
            BuscarBox2.SendKeys("Chris221807.");

            var searchB = driverr.FindElement(By.Id("loginbtn"));

            //Captura en el inicio de sesion
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Inicio Sesion.png"));

            //Hacer clic en el botón con JavaScript
            Botonjs(driverr, searchB);

            //Foto
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Vuelve al Inicio.png"));

            //Ir a mi perfil
            driverr.Navigate().GoToUrl("https://plataformavirtual.itla.edu.do/user/profile.php?id=11875");

            //Foto Editar Perfil
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Edita el perfil.png"));

            //Editar mi perfil
            driverr.Navigate().GoToUrl("https://plataformavirtual.itla.edu.do/user/edit.php?id=11875&returnto=profile");

            //Ir al textbox
            var BoxPerfil = driverr.FindElement(By.Id("id_description_editoreditable"));
            BoxPerfil.SendKeys("Me gusta la Programación y la tecnologia");

            //Foto de box
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Caja de Texto.png"));

            //Boton para guardan
            var BotonInfo = driverr.FindElement(By.Id("id_submitbutton"));
            Botonjs(driverr, BotonInfo);

            //Foto de guardar info
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Guarda la informacion.png"));

            //Ir al curso
            driverr.Navigate().GoToUrl("https://plataformavirtual.itla.edu.do/course/view.php?id=5571");

            //Captura del curso
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Curso de Programación III.png"));

            //Captura de la tarea#4
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Tarea.png"));

            //Ir a la tarea 4
            driverr.Navigate().GoToUrl("https://plataformavirtual.itla.edu.do/mod/assign/view.php?id=412153&action=editsubmission");

            var BuscarTextBox = driverr.FindElement(By.Id("id_onlinetext_editoreditable"));
            BuscarTextBox.SendKeys(" Hola maestro y tutor.");

            //Captura del textbox con el texto escrito
            CapturaF(driverr, Path.Combine(evidencia_del_Folder, "Agregar texto.png"));

            //Generar informe HTML
            HTML(evidencia_del_Folder);

            //Obtener la ruta completa del informe HTML
            string reportFP = Path.Combine(evidencia_del_Folder, "Informe.html");
            Console.WriteLine($"Informe HTML creado en: {reportFP}");

            //Abrir el HTML
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = reportFP,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }
    }

    //Función para hacer clic en un elemento mediante JavaScript
    static void Botonjs(IWebDriver driver, IWebElement element)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].click();", element);
    }

    //Funcion para tirar captura
    static void CapturaF(IWebDriver driverrs, string file)
    {
        try
        {
            ITakesScreenshot screenshotD = (ITakesScreenshot)driverrs;
            Screenshot screenshott = screenshotD.GetScreenshot();
            screenshott.SaveAsFile(file, ScreenshotImageFormat.Png);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al capturar la pantalla: {ex.Message}");
        }
    }

    //Genera el HTML
    static void HTML(string evidencia_del_Folder)
    {
        try
        {
            string reportF = Path.Combine(evidencia_del_Folder, "Informe.html");
            using (StreamWriter WriteS = new StreamWriter(reportF))
            {
                WriteS.WriteLine("<html>");
                WriteS.WriteLine("<head>");
                WriteS.WriteLine("<title>Reportes</title>");

                WriteS.WriteLine("<style>body { background-color: rgb(40, 142, 182); }</style>");
                WriteS.WriteLine("<style>h1 { font-family: Arial, sans-serif; }</style>");

                WriteS.WriteLine("</head>");
                WriteS.WriteLine("<body>");

                WriteS.WriteLine("<div style='background-color: white; padding: 25px;'><br><center><h1>Informe de Evidencia:</h1></center></div><hr><br>");

                //Obtener la lista de archivos de imágenes en la carpeta
                string[] imagens = Directory.GetFiles(evidencia_del_Folder, "*.png");

                //Agregar cada imagen al informe HTML con un encabezado
                foreach (string imageF in imagens)
                {
                    //Obtener el nombre del archivo sin la extensión
                    string nombreArchivo = Path.GetFileNameWithoutExtension(imageF);

                    //Agregar nombre de cada imagen
                    WriteS.WriteLine($"<center><h2>{nombreArchivo}:</h2></center>");
                    //Agregar imagen
                    WriteS.WriteLine($"<center><img src=\"{Path.GetFileName(imageF)}\" alt=\"Captura de pantalla\"></center><br>");
                }
                WriteS.WriteLine("</body>");
                WriteS.WriteLine("</html>");
            }
            Console.WriteLine($"Reporte HTML creado en: {reportF}");
        }
        catch (Exception error)
        {
            Console.WriteLine($"Error al generar el reporte HTML: {error.Message}");
        }
    }
}
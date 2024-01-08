using System.Diagnostics;
using IronPython.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Scripting.Hosting;
using Presentaion.Models;
using static Microsoft.Scripting.Hosting.Shell.SuperConsole;

namespace Presentaion.Controllers {
    public class HomeController : Controller {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment) {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(ImageProcessingModel input) {
            if(!string.IsNullOrEmpty(input.picture) && !input.picture.EndsWith(".jpg"))
            {
                byte[] bytes = Convert.FromBase64String(input.picture.Split("base64,")[1]);

                // Replace "outputFile.txt" with the desired file name and extension
                string filePath = @"Pythons\image.jpg"; // string.Format("{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));

                // Write the bytes to a file
                System.IO.File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath), bytes);

                input.picture = filePath;

            }
            if (!string.IsNullOrWhiteSpace(input.picture))
            {

                string pythonScript = System.IO.File.ReadAllText(Path.Combine(_hostingEnvironment.WebRootPath, @"Pythons\script.py"));

                pythonScript = pythonScript.Replace("{{corners}}", $"{input.corners}");

                System.IO.File.WriteAllText(Path.Combine(_hostingEnvironment.WebRootPath, @"Pythons\script2.py"), pythonScript);

                // Replace "your_batch_file.bat" with the actual path to your batch file
                string batchFilePath = Path.Combine(_hostingEnvironment.WebRootPath, @"Pythons\run.bat");

                // Create a new process to run the batch file
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe"; // Use the command prompt
                process.StartInfo.Arguments = $"/C {batchFilePath}"; // /C executes the command and terminates
                process.StartInfo.RedirectStandardOutput = true; // Redirect output if needed
                process.StartInfo.UseShellExecute = false; // UseShellExecute should be false to redirect output
                process.Start();
                process.WaitForExit(); // Wait for the batch file to finish executing

                // Optionally, retrieve output if you redirected it
                string output = process.StandardOutput.ReadToEnd();
                //string error = process.StandardError.ReadToEnd();

                var exitCode = process.ExitCode;
                process.Close();
                /***********************

                // Create a new process instance for Python interpreter
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "python",
                        Arguments = $"Pythons\\script.py \"{Path.Combine(_hostingEnvironment.WebRootPath, input.picture)}\""
                    }
                };

                // Start the Python process
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                // Wait for the process to finish and get the output
                var output = process.StandardOutput.ReadToEnd();
                / **************** /

                string pythonPath = @"python";

                // Python script to run
                //string pythonScript = "Pythons/script.py  \"{Path.Combine(_hostingEnvironment.WebRootPath, input.picture)}\"";
                
                string pythonScript = System.IO.File.ReadAllText(Path.Combine(System.AppContext.BaseDirectory, $"Pythons/script.py"));

                pythonScript = pythonScript.Replace("{{filename}}", Path.Combine(_hostingEnvironment.WebRootPath, input.picture).Replace("\\", "/"));

                // Command to run Python script
                string command = $"{pythonPath} {pythonScript}";

                // Start a new process
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = pythonScript,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                // Start the process
                using (Process process = Process.Start(start))
                {
                    // Read the output and error streams
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }

                    using (StreamReader reader = process.StandardError)
                    {
                        string error = reader.ReadToEnd();
                        Console.WriteLine(error);
                    }
                }

                

                /*************** /

                // Python code to execute
                string pythonCode = System.IO.File.ReadAllText(Path.Combine(System.AppContext.BaseDirectory, $"Pythons/script.py"));

                pythonCode = pythonCode.Replace("{{filename}}", Path.Combine(_hostingEnvironment.WebRootPath, input.picture).Replace("\\", "/"));
                // Create an instance of Python engine
                var engine = Python.CreateEngine();
                engine.ImportModule("cv2");
                var paths = engine.GetSearchPaths();
                paths.Add(@"C:\Users\ImanKazemi\AppData\Local\Programs\Python\Python312\Lib");
                //paths.Add(@"C:\Python27\Lib"); // or you can add the CPython libs instead
                engine.SetSearchPaths(paths);

                // Execute the Python code
                ScriptScope scope = engine.CreateScope();
                engine.Execute(pythonCode, scope);

                // Access the result variable from the Python code
                dynamic result = scope.GetVariable("result");

                ****************/


            }

            return View(input);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
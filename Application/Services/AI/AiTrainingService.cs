using System.Diagnostics;

namespace Application.Services.AI
{
    public class AiTrainingService : IAiTrainingService
    {
        public async Task<bool> RetrainModelAsync()
        {
            try
            {
                await RunScript("pull_data.py");
                await RunScript("prepare_dataset.py");
                await RunScript("train_model.py");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Retrain Error] {ex.Message}");
                return false;
            }
        }
        private Task RunScript(string fileName)
        {
            var tcs = new TaskCompletionSource();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = fileName,
                    WorkingDirectory = @"C:\Users\draig\Desktop\relaxify_ai", // 🟡 kendi dizinini yaz
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                    Console.WriteLine($"[stdout] {e.Data}");
            };

            process.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                    Console.WriteLine($"[stderr] {e.Data}");
            };

            process.Exited += (s, e) =>
            {
                if (process.ExitCode == 0)
                    tcs.SetResult();
                else
                    tcs.SetException(new Exception($"Script {fileName} failed."));
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }
    }
}

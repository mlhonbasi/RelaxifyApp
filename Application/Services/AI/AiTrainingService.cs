using System.Diagnostics;

namespace Application.Services.AI
{
    public class AiTrainingService : IAiTrainingService
    {
        public async Task<bool> RetrainModelAsync()
        {
            try
            {
                await RunScript("pull_data.py", "content_recommendation");
                await RunScript("prepare_dataset.py", "content_recommendation");
                await RunScript("train_model.py", "content_recommendation");
                await ReloadModelInFlaskAsync("http://localhost:5000/reload-model");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Retrain Error - Recommendation] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RetrainStressModelAsync()
        {
            try
            {
                await RunScript("pull_data.py", "stress_prediction");
                await RunScript("prepare_dataset.py", "stress_prediction");
                await RunScript("train_model.py", "stress_prediction");
                await ReloadModelInFlaskAsync("http://localhost:5000/reload-stress-model");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Retrain Error - Stress] {ex.Message}");
                return false;
            }
        }

        private Task RunScript(string fileName, string workingDir)
        {
            var tcs = new TaskCompletionSource();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = fileName,
                    WorkingDirectory = $@"C:\Users\draig\Desktop\relaxify_ai\{workingDir}",
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
                    tcs.SetException(new Exception($"Script {fileName} failed in {workingDir}"));
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }

        private async Task ReloadModelInFlaskAsync(string endpoint)
        {
            using var client = new HttpClient();
            var response = await client.PostAsync(endpoint, null);
            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Flask model reload başarısız: {msg}");
            }
            else
            {
                Console.WriteLine($"✅ Flask modeli yeniden yüklendi → {endpoint}");
            }
        }
    }
}

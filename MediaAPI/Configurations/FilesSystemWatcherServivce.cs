namespace MediaAPI.Configurations
{
    public class FileSystemWatcherService
    {
        private readonly string path;
        private readonly FileSystemWatcher watcher;

        public FileSystemWatcherService(string path)
        {
            this.path = path;

            watcher = new FileSystemWatcher(path)
            {
                IncludeSubdirectories = false,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
            };

            watcher.Created += OnFileCreated;
            watcher.Changed += OnFileChanged;
            watcher.Deleted += OnFileDeleted;

            watcher.EnableRaisingEvents = true;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File created: {e.Name}");
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File changed: {e.Name}");
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File deleted: {e.Name}");
        }
    }
}

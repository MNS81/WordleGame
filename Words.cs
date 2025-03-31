using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleGame
{
    internal class Words
    {
        public List<String> words = new List<String>();
        [Inject] ModalService ModalService { get; set; } = default!;

        public Words()
        {
            string path = Path.Combine(FileSystem.Current.AppDataDirectory, "words.db");   // Универсальный доступ к файлу
            if (!File.Exists(path))     // Копирование файла из ресурсов, если он отсутствует в целевой директории
            {
                using var source = FileSystem.OpenAppPackageFileAsync("words.db").Result;
                using var destination = File.Create(path);
                source.CopyTo(destination);
            }
            if (File.Exists(path))  // Чтение файла
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        words.Add(line.ToUpper().Replace("Ё", "Е"));
                    }
                }
            }
            else ShowModal(ModalType.Danger, "Файл words.db не найден.");
        }

        private async Task ShowModal(ModalType modalType, string msg)
        {
            var modalOption = new ModalOption
            {
                Title = "Внимание!",
                Message = msg,
                Type = modalType,
            };
            await ModalService.ShowAsync(modalOption);
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotWpfSB2.View;
using File = System.IO.File;

namespace TelegramBotWpfSB2
{
    internal class TelegramClient
    {
        private readonly MainWindow _mainWindow;

        private readonly TelegramBotClient _bot;

        private readonly string pathToDirectory = @"C:\Users\delicia\Desktop\DownloadTelegramBot\";

        public TelegramClient(MainWindow mainWindow, string pathToken = @"C:\Users\delicia\Desktop\C#\token_telegram\token.txt")
        {
            BotMessageLog = new ObservableCollection<MessageLog>();
            _mainWindow = mainWindow;

            _bot = new TelegramBotClient(System.IO.File.ReadAllText(pathToken));

            var cts = new CancellationTokenSource();


            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            _bot.StartReceiving(HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);
        }
        public TelegramClient(){}
        
        public ObservableCollection<MessageLog> BotMessageLog { get; set; }

        async Task HandleUpdateAsync(ITelegramBotClient iBotClient, Update update, CancellationToken cancellationToken)
        {
            long chatId = 0;
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    break;
                case UpdateType.Message:
                    chatId = update.Message.Chat.Id;
                    break;
            }
            var dictionaryTypes = LoadDictionary(pathToDirectory + @"AllFilesDictionary.txt");

            if (update.Type == UpdateType.Message)
            {
                if (update.Message!.Type == MessageType.Text)
                {
                    Debug.WriteLine($"Received a {update.Message.Text} message in chat {chatId}.");

                    if (update.Message.Text == "Москва" || update.Message.Text == "Казань" || update.Message.Text == "Санкт-Петербург"
                        || update.Message.Text == "Пермь" || update.Message.Text == "Milan" || update.Message.Text == "New York"
                        || update.Message.Text == "LA" || update.Message.Text == "Paris")
                    {
                        HttpClient httpClient;
                        string apiKey;
                        string url;
                        HttpResponseMessage result;
                        switch (update.Message.Text)
                        {
                            case "Москва":
                                httpClient = new HttpClient();
                                apiKey = File.ReadAllText(@"C:\Users\delicia\Desktop\C#\SB9v2\bin\Debug\net6.0\weatherApiKey.txt");
                                url = $@"https://api.openweathermap.org/data/2.5/weather?q=Moscow,ru&lang=ru&appid={apiKey}";
                                result = await httpClient.GetAsync(url);
                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    HttpContent responseContent = result.Content;
                                    var jsonResult = JObject.Parse(await responseContent.ReadAsStringAsync());
                                    var weather = jsonResult["weather"].ToArray();
                                    var temp = Convert.ToInt32(jsonResult["main"]["temp"]);
                                    var feelTemp = Convert.ToInt32(jsonResult["main"]["feels_like"]);

                                    var message = "Погода в Москве: \n" +
                                                  "Погода: " + weather[0]["description"] + "\n" +
                                                  "Температура : " + (temp - 273) + "С\n" +
                                                  "Ощущается как: " + (feelTemp - 273) + "С";

                                    var sentMessage = await iBotClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
                                        replyMarkup: new ReplyKeyboardRemove(),
                                        cancellationToken: cancellationToken);
                                }
                                break;
                            case "Казань":
                                httpClient = new HttpClient();
                                apiKey = File.ReadAllText(@"C:\Users\delicia\Desktop\C#\SB9v2\bin\Debug\net6.0\weatherApiKey.txt");
                                url = $@"https://api.openweathermap.org/data/2.5/weather?q=Kazan',ru&lang=ru&appid={apiKey}";
                                result = await httpClient.GetAsync(url);
                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    HttpContent responseContent = result.Content;
                                    var jsonResult = JObject.Parse(await responseContent.ReadAsStringAsync());
                                    var weather = jsonResult["weather"].ToArray();
                                    var temp = Convert.ToInt32(jsonResult["main"]["temp"]);
                                    var feelTemp = Convert.ToInt32(jsonResult["main"]["feels_like"]);

                                    var message = "Погода в Казани: \n" +
                                                  "Погода: " + weather[0]["description"] + "\n" +
                                                  "Температура : " + (temp - 273) + "С\n" +
                                                  "Ощущается как: " + (feelTemp - 273) + "С";

                                    var sentMessage = await iBotClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
                                        replyMarkup: new ReplyKeyboardRemove(),
                                        cancellationToken: cancellationToken);
                                }
                                break;
                            case "Санкт-Петербург":
                                httpClient = new HttpClient();
                                apiKey = File.ReadAllText(@"C:\Users\delicia\Desktop\C#\SB9v2\bin\Debug\net6.0\weatherApiKey.txt");
                                url = $@"https://api.openweathermap.org/data/2.5/weather?lat=60&lon=30&lang=ru&appid={apiKey}";
                                result = await httpClient.GetAsync(url);
                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    HttpContent responseContent = result.Content;
                                    var jsonResult = JObject.Parse(await responseContent.ReadAsStringAsync());
                                    var weather = jsonResult["weather"].ToArray();
                                    var temp = Convert.ToInt32(jsonResult["main"]["temp"]);
                                    var feelTemp = Convert.ToInt32(jsonResult["main"]["feels_like"]);

                                    var message = "Погода в Санкт-Петербурге: \n" +
                                                  "Погода: " + weather[0]["description"] + "\n" +
                                                  "Температура : " + (temp - 273) + "С\n" +
                                                  "Ощущается как: " + (feelTemp - 273) + "С";

                                    var sentMessage = await iBotClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
                                        replyMarkup: new ReplyKeyboardRemove(),
                                        cancellationToken: cancellationToken);
                                }
                                break;
                            case "Пермь":
                                httpClient = new HttpClient();
                                apiKey = File.ReadAllText(@"C:\Users\delicia\Desktop\C#\SB9v2\bin\Debug\net6.0\weatherApiKey.txt");
                                url = $@"https://api.openweathermap.org/data/2.5/weather?lat=58&lon=56&lang=ru&appid={apiKey}";
                                result = await httpClient.GetAsync(url);
                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    HttpContent responseContent = result.Content;
                                    var jsonResult = JObject.Parse(await responseContent.ReadAsStringAsync());
                                    var weather = jsonResult["weather"].ToArray();
                                    var temp = Convert.ToInt32(jsonResult["main"]["temp"]);
                                    var feelTemp = Convert.ToInt32(jsonResult["main"]["feels_like"]);

                                    var message = "Погода в Перми: \n" +
                                                  "Погода: " + weather[0]["description"] + "\n" +
                                                  "Температура : " + (temp - 273) + "С\n" +
                                                  "Ощущается как: " + (feelTemp - 273) + "С";

                                    var sentMessage = await iBotClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
                                        replyMarkup: new ReplyKeyboardRemove(),
                                        cancellationToken: cancellationToken);
                                }
                                break;
                            case "Milan":
                                httpClient = new HttpClient();
                                apiKey = File.ReadAllText(@"C:\Users\delicia\Desktop\C#\SB9v2\bin\Debug\net6.0\weatherApiKey.txt");
                                url = $@"https://api.openweathermap.org/data/2.5/weather?lat=45&lon=9&lang=ru&appid={apiKey}";
                                result = await httpClient.GetAsync(url);
                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    HttpContent responseContent = result.Content;
                                    var jsonResult = JObject.Parse(await responseContent.ReadAsStringAsync());
                                    var weather = jsonResult["weather"].ToArray();
                                    var temp = Convert.ToInt32(jsonResult["main"]["temp"]);
                                    var feelTemp = Convert.ToInt32(jsonResult["main"]["feels_like"]);

                                    var message = "Погода в Милане: \n" +
                                                  "Погода: " + weather[0]["description"] + "\n" +
                                                  "Температура : " + (temp - 273) + "С\n" +
                                                  "Ощущается как: " + (feelTemp - 273) + "С";

                                    var sentMessage = await iBotClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
                                        replyMarkup: new ReplyKeyboardRemove(),
                                        cancellationToken: cancellationToken);
                                }
                                break;
                            case "New York":
                                httpClient = new HttpClient();
                                apiKey = File.ReadAllText(@"C:\Users\delicia\Desktop\C#\SB9v2\bin\Debug\net6.0\weatherApiKey.txt");
                                url = $@"https://api.openweathermap.org/data/2.5/weather?lat=41&lon=-74&lang=ru&appid={apiKey}";
                                result = await httpClient.GetAsync(url);
                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    HttpContent responseContent = result.Content;
                                    var jsonResult = JObject.Parse(await responseContent.ReadAsStringAsync());
                                    var weather = jsonResult["weather"].ToArray();
                                    var temp = Convert.ToInt32(jsonResult["main"]["temp"]);
                                    var feelTemp = Convert.ToInt32(jsonResult["main"]["feels_like"]);

                                    var message = "Погода в Нью Йорке: \n" +
                                                  "Погода: " + weather[0]["description"] + "\n" +
                                                  "Температура : " + (temp - 273) + "С\n" +
                                                  "Ощущается как: " + (feelTemp - 273) + "С";

                                    var sentMessage = await iBotClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
                                        replyMarkup: new ReplyKeyboardRemove(),
                                        cancellationToken: cancellationToken);
                                }
                                break;
                            case "LA":
                                httpClient = new HttpClient();
                                apiKey = File.ReadAllText(@"C:\Users\delicia\Desktop\C#\SB9v2\bin\Debug\net6.0\weatherApiKey.txt");
                                url = $@"https://api.openweathermap.org/data/2.5/weather?lat=34&lon=-118&lang=ru&appid={apiKey}";
                                result = await httpClient.GetAsync(url);
                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    HttpContent responseContent = result.Content;
                                    var jsonResult = JObject.Parse(await responseContent.ReadAsStringAsync());
                                    var weather = jsonResult["weather"].ToArray();
                                    var temp = Convert.ToInt32(jsonResult["main"]["temp"]);
                                    var feelTemp = Convert.ToInt32(jsonResult["main"]["feels_like"]);

                                    var message = "Погода в Лос-Анджелесе: \n" +
                                                  "Погода: " + weather[0]["description"] + "\n" +
                                                  "Температура : " + (temp - 273) + "С\n" +
                                                  "Ощущается как: " + (feelTemp - 273) + "С";

                                    var sentMessage = await iBotClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
                                        replyMarkup: new ReplyKeyboardRemove(),
                                        cancellationToken: cancellationToken);
                                }
                                break;
                            case "Paris":
                                httpClient = new HttpClient();
                                apiKey = File.ReadAllText(@"C:\Users\delicia\Desktop\C#\SB9v2\bin\Debug\net6.0\weatherApiKey.txt");
                                url = $@"https://api.openweathermap.org/data/2.5/weather?lat=49&lon=2&lang=ru&appid={apiKey}";
                                result = await httpClient.GetAsync(url);
                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    HttpContent responseContent = result.Content;
                                    var jsonResult = JObject.Parse(await responseContent.ReadAsStringAsync());
                                    var weather = jsonResult["weather"].ToArray();
                                    var temp = Convert.ToInt32(jsonResult["main"]["temp"]);
                                    var feelTemp = Convert.ToInt32(jsonResult["main"]["feels_like"]);

                                    var message = "Погода в Париже: \n" +
                                                  "Погода: " + weather[0]["description"] + "\n" +
                                                  "Температура : " + (temp - 273) + "С\n" +
                                                  "Ощущается как: " + (feelTemp - 273) + "С";

                                    var sentMessage = await iBotClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: message,
                                        replyMarkup: new ReplyKeyboardRemove(),
                                        cancellationToken: cancellationToken);
                                }
                                break;
                        }
                    }

                    if (update.Message.Text != "/weather" && update.Message.Text != "/download")
                    {
                        _mainWindow.Dispatcher.Invoke(() =>
                        {
                            BotMessageLog.Add(
                            new MessageLog(DateTime.Now.ToString(),
                            update.Message.Text,
                            update.Message.Chat.FirstName,
                            update.Message.Chat.Id,
                            update.Message.Type.ToString()));
                        });

                        var messageLogPath = @"messageLog.txt";
                        var jsonMessageLog = new List<string?>();
                        using var sr = new StreamReader(messageLogPath);
                        while (!sr.EndOfStream)
                        {
                            jsonMessageLog.Add(await sr.ReadLineAsync().ConfigureAwait(false));
                        }
                        sr.Close();
                        jsonMessageLog.Add(JsonSerializer.Serialize(BotMessageLog.Last()));
                        await using var sw = new StreamWriter(messageLogPath, false);
                        foreach (var e in jsonMessageLog) await sw.WriteLineAsync(e);
                        sw.Close();

                        var messageText = "Привет, я бот, который выполняет функции хранилища, а еще умею говорить прогноз погоды." +
                                          "\nТы можешь прислать мне файл и я его сохраню, а потом пришлю его тебе, когда попросишь :)" +
                                          "\nНиже команды, которые я умею выполнять:" +
                                          "\n/weather - покажу погоду" +
                                          "\n/download - пришлю тебе нужный файл" +
                                          "\nили дождись ответа реального человека";

                        var sentMessage = await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: messageText,
                            cancellationToken: cancellationToken);
                    }

                    else if (update.Message.Text == "/weather")
                    {
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                        {
                        new KeyboardButton[] { "Москва", "Казань" },
                        new KeyboardButton[] { "Санкт-Петербург", "Пермь" },
                        new KeyboardButton[] { "Milan", "New York" },
                        new KeyboardButton[] { "LA", "Paris" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        var sentMessage = await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Выбери город:",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }

                    else if (update.Message.Text == "/download")
                    {

                        var message = "Напишите название файла. Вот список:\n";


                        var inlineKeyboardButtons = new List<InlineKeyboardButton[]>();

                        foreach (var e in dictionaryTypes)
                        {
                            var inlineButton = new[] { InlineKeyboardButton.WithCallbackData(text: e.Key, callbackData: e.Key) };
                            inlineKeyboardButtons.Add(inlineButton);
                        }

                        InlineKeyboardMarkup inlineKeyboard = new(
                            inlineKeyboardButtons
                        );


                        Message sentMessage = await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: message,
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
                    }
                }

                if (update.Message!.Type != MessageType.Text)
                {
                    var type = update.Message!.Type.ToString();
                    var now = DateTime.Now;
                    long? fileSize;
                    string messageText = null;
                    string fileId = null;
                    string fileName = null;
                    string? pathFileName = null;
                    switch (type)
                    {
                        case "Voice":
                            messageText = "О, голосовушка! Сохраним";
                            fileName = "Voice_" + now.ToString("dd_MM_yyyy_HH_mm_ss");
                            pathFileName = pathToDirectory +  $@"{type}\" + fileName + ".mp3";
                            fileId = update.Message.Voice.FileId;
                            fileSize = update.Message.Voice.FileSize;
                            Console.WriteLine($"Received a voice message in chat {chatId}.");
                            break;
                        case "VideoNote":
                            messageText = "О, круглая видюшка! Сохраним";
                            fileName = "VideoNote_" + now.ToString("dd_MM_yyyy_HH_mm_ss");
                            pathFileName = pathToDirectory + $@"{type}\" + fileName + ".mp4";
                            fileId = update.Message.VideoNote.FileId;
                            Console.WriteLine($"Received a videonote message in chat {chatId}.");
                            break;
                        case "Video":
                            messageText = "О, видюшка! Сохраним";
                            fileName = "Video_" + now.ToString("dd_MM_yyyy_HH_mm_ss");
                            pathFileName = pathToDirectory + $@"{type}\" + fileName + ".mp4";
                            fileId = update.Message.Video.FileId;
                            Console.WriteLine($"Received a video message in chat {chatId}.");
                            break;
                        case "Photo":
                            messageText = "О, фоточка! Сохраним";
                            fileName = "Photo_" + now.ToString("dd_MM_yyyy_HH_mm_ss");
                            pathFileName = pathToDirectory + $@"{type}\" + fileName + ".jpg";
                            fileId = update.Message.Photo[1].FileId;
                            Console.WriteLine($"Received a photo message in chat {chatId}.");
                            break;
                        case "Document":
                            messageText = "О, файлик! Сохраним";
                            string[] findFormat = update.Message.Document.FileName.Split('.');
                            fileName = "Document_" + now.ToString("dd_MM_yyyy_HH_mm_ss");
                            pathFileName = pathToDirectory + $@"{type}\{fileName}.{findFormat.Last()}";
                            fileId = update.Message.Document.FileId;
                            Console.WriteLine($"Received a document message in chat {chatId}.");
                            break;
                        case "Audio":
                            messageText = "О, музычка! Сохраним";
                            fileName = "Audio_" + now.ToString("dd_MM_yyyy_HH_mm_ss");
                            pathFileName = pathToDirectory + $@"{type}\" + fileName + ".mp3";
                            fileId = update.Message.Audio.FileId;
                            Console.WriteLine($"Received an audio message in chat {chatId}.");
                            break;
                    }

                    if (AddToDictionary(LoadDictionary(pathToDirectory + "AllFilesDictionary.txt"), fileName, type, (pathToDirectory + "AllFilesDictionary.txt")))
                    {
                        if (AddToDictionary(LoadDictionary(pathToDirectory + @$"{type}\FilesDictionary.txt"), fileName, fileId, (pathToDirectory + @$"{type}\FilesDictionary.txt")))
                        {
                            var file = await iBotClient.GetFileAsync(fileId);
                            var fs = new FileStream(pathFileName, FileMode.Create);
                            await iBotClient.DownloadFileAsync(file.FilePath, fs);
                            fs.Close();

                            fs.Dispose();

                            var sentMessage = await iBotClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: messageText,
                                cancellationToken: cancellationToken);
                        }
                    }
                    else
                    {
                        var sentMessage = await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Файл с таким именем уже есть, не могу загрузить. Поменяйте название файла",
                            cancellationToken: cancellationToken);
                    }
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                var sentMessage = await iBotClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Загружаем",
                        cancellationToken: cancellationToken);

                Dictionary<string, string> dictionaryFiles;
                InputOnlineFile inputOnlineFile;
                var typeKey = dictionaryTypes.GetValueOrDefault(update.CallbackQuery.Data);
                switch (typeKey)
                {
                    case "Photo":
                        dictionaryFiles = LoadDictionary(pathToDirectory+ @$"{typeKey}\FilesDictionary.txt");
                        inputOnlineFile = new InputOnlineFile(dictionaryFiles.GetValueOrDefault(update.CallbackQuery.Data));
                        await iBotClient.SendPhotoAsync(chatId, inputOnlineFile);
                        await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Держи",
                            cancellationToken: cancellationToken);
                        break;
                    case "Video":
                        dictionaryFiles = LoadDictionary(pathToDirectory + @$"{typeKey}\FilesDictionary.txt");
                        inputOnlineFile = new InputOnlineFile(dictionaryFiles.GetValueOrDefault(update.CallbackQuery.Data));
                        await iBotClient.SendVideoAsync(chatId, inputOnlineFile);
                        await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Держи",
                            cancellationToken: cancellationToken);
                        break;
                    case "Audio":
                        dictionaryFiles = LoadDictionary(pathToDirectory + @$"{typeKey}\FilesDictionary.txt");
                        inputOnlineFile = new InputOnlineFile(dictionaryFiles.GetValueOrDefault(update.CallbackQuery.Data));
                        await iBotClient.SendAudioAsync(chatId, inputOnlineFile);
                        await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Держи",
                            cancellationToken: cancellationToken);
                        break;
                    case "Document":
                        dictionaryFiles = LoadDictionary(pathToDirectory + @$"{typeKey}\FilesDictionary.txt");
                        inputOnlineFile = new InputOnlineFile(dictionaryFiles.GetValueOrDefault(update.CallbackQuery.Data));
                        await iBotClient.SendDocumentAsync(chatId, inputOnlineFile);
                        await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Держи",
                            cancellationToken: cancellationToken);
                        break;
                    case "VideoNote":
                        dictionaryFiles = LoadDictionary(pathToDirectory + @$"{typeKey}\FilesDictionary.txt");
                        inputOnlineFile = new InputOnlineFile(dictionaryFiles.GetValueOrDefault(update.CallbackQuery.Data));
                        await iBotClient.SendVideoAsync(chatId, inputOnlineFile);
                        await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Держи",
                            cancellationToken: cancellationToken);
                        break;
                    case "Voice":
                        dictionaryFiles = LoadDictionary(pathToDirectory + @$"{typeKey}\FilesDictionary.txt");
                        inputOnlineFile = new InputOnlineFile(dictionaryFiles.GetValueOrDefault(update.CallbackQuery.Data));
                        await iBotClient.SendAudioAsync(chatId, inputOnlineFile);
                        await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Держи",
                            cancellationToken: cancellationToken);
                        break;
                    default:
                        await iBotClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Не получилось загрузить",
                            cancellationToken: cancellationToken);
                        break;

                }
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Debug.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        static bool AddToDictionary(Dictionary<string, string> dictionary, string fileName, string fileId, string path)
        {
            bool flag;
            if (fileName != null && fileId != null)
            {
                flag = dictionary.TryAdd(fileName, fileId);
                using var sw = new StreamWriter(path, false);
                foreach (var e in dictionary)
                {
                    sw.WriteLine(e);
                }
                sw.Close();
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        static Dictionary<string, string> LoadDictionary(string path)
        {
            var dictionary = new Dictionary<string, string>();
            using var sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string[] pair = sr.ReadLine().Split(',', ' ', '[', ']');
                dictionary.TryAdd(pair[1], pair[3]);
            }

            return dictionary;
        }



        public void SendMessage(string Text, string Id)
        {
            var test = Int64.TryParse(Id, out var id);
            if (test) _bot.SendTextMessageAsync(id, Text);
        }

        public ObservableCollection<MessageLog> SentMessageLog()
        {
            var messageLogPath = @"messageLog.txt";
            var logs = new ObservableCollection<MessageLog?>();

            using Stream stream = new FileStream(messageLogPath,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.ReadWrite);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };
            if (File.Exists(messageLogPath) && stream.Length > 0)
            {
                string fileContents;
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        fileContents = reader.ReadLine();
                        MessageLog obj = JsonSerializer.Deserialize<MessageLog>(fileContents, options);
                        logs.Add(obj);
                    }
                }
                return logs;
            }
            else
            {
                return logs;
            }
        }
    }
    }

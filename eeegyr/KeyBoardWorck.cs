using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace eeegyr
{
    public static class KeyBoardWork 
    {
          
        public static State KeeBoardForTest()
        {
            var allStudios = new Allstudios();
            var allPlace = StudioInfo.GetPlace(allStudios.FullStudios());
            var stepPlace = new Step()
            {
                Text = "Выбери удобные для тебя станции метро, рядом с которыми находится студия.",
                Buttons = allPlace.Select(x => new ButtonChoice() {Flag = false, Text = x}).ToList(),
                Nav = new Navigation() {Text = "\U0001F51C"}
            };
            var allPrice = StudioInfo.GetPrice(allStudios.FullStudios());
            var stepPrice = new Step()
            {
                Text = "Привет! Ответь на несколько вопросов и мы подберём студию, которая идеально подойдёт для осуществления твоей задумки.\n Итак, инструкция:\n1. Выбранный тобой вариант будет помечен символом 📌\n2.Как только ты выбрал(а) все варианты, которые тебе подходят, жми кнопку «дальше».\n\nНу вот и все, поехали!\nВыбери диапазон цен, который тебя устраивает, не забывай, что можно выбирать несколько вариантов, и потом жать 🔜 ",
                Buttons = allPrice.Select(x => new ButtonChoice() {Flag = false, Text = x}).ToList(),
                Nav = new Navigation() {Text = "\U0001F51C"}
            };
            var allStyle = StudioInfo.GetStyle(allStudios.FullStudios()).Distinct();
            var stepStyle = new Step()
            {
                Text = "Выбор вида съемки \n Для какой съёмки ищешь фотостудию?",
                Buttons = allStyle.Select(x => new ButtonChoice() {Flag = false, Text = x}).ToList(),
                Nav = new Navigation() {Text = "\U0001F51C"}
            };
            var allType = StudioInfo.GetType(allStudios.FullStudios()).Distinct();
            var stepType = new Step()
            {
                Text = "Выбери предпочитаемый стиль студии",
                Buttons = allType.Select(x => new ButtonChoice() {Flag = false, Text = x}).ToList(),
                Nav = new Navigation() {Text = " \U0001F51C "}
            };
            List<Step> steps = new List<Step>();
            steps.Add(stepPrice);
            steps.Add(stepPlace);
            steps.Add(stepStyle);
            steps.Add(stepType);
            return new State(){Steps = steps,CurrentStep = stepPrice};
        }

        public static InlineKeyboardMarkup KeeBoardForTest2(Step state)
        {

            var rows = new List<InlineKeyboardButton[]>();
            var cols = new List<InlineKeyboardButton>();
            var index = 0;
            foreach (var button in state.Buttons)
            {
                index++;
                if (button.Flag == false)
                {
                    cols.Add(InlineKeyboardButton.WithCallbackData(button.Text, button.Text));
                }
                else
                {
                    cols.Add(InlineKeyboardButton.WithCallbackData(  "\U0001F4CC" + button.Text, button.Text));
                }

                if (index % 3 != 0)
                {
                    continue;
                }
                else
                {
                    rows.Add(cols.ToArray());
                    cols = new List<InlineKeyboardButton>();
                }
            }

            
            if (state.Nav.Text == "\U0001F51C")
            {
                cols.Add(InlineKeyboardButton.WithCallbackData(state.Nav.Text, state.Nav.Text));
                rows.Add(cols.ToArray());
            }

            else
            {
                cols.Add(InlineKeyboardButton.WithCallbackData(state.Nav.Text, " \U0001F51C "));
                rows.Add(cols.ToArray());
            }
            return new InlineKeyboardMarkup(rows);
        }

        public static InlineKeyboardMarkup KeeBoardForPrice()
                {
                    var rows = new List<InlineKeyboardButton[]>();
                    var cols = new List<InlineKeyboardButton>();
                    var allStudios = new Allstudios();
                    var allPrice = StudioInfo.GetPrice(allStudios.ParseStudios("test.txt"));
                    var index = 0;
                    foreach (var priceForStudio in allPrice)
                    {
                        cols.Add(InlineKeyboardButton.WithCallbackData(priceForStudio, priceForStudio));
        
                        if (index % 4 != 0) continue;
                        rows.Add(cols.ToArray());
                        cols = new List<InlineKeyboardButton>();
                        index++;
        
                    }
                    cols.Add(InlineKeyboardButton.WithCallbackData("Select to place", "Select to place"));
                    rows.Add(cols.ToArray());
                    return new InlineKeyboardMarkup(rows);
                }
                
        
        public static InlineKeyboardMarkup KeeBoardForPlace()
        {
            var rows = new List<InlineKeyboardButton[]>();
            var cols = new List<InlineKeyboardButton>();
            var allStudios = new Allstudios();
            var allPrice = StudioInfo.GetPlace(allStudios.ParseStudios("test.txt"));
            var index = 0;
            foreach (var priceForStudio in allPrice)
            {
                cols.Add(InlineKeyboardButton.WithCallbackData(priceForStudio, priceForStudio));

                if (index % 4 != 0) continue;
                rows.Add(cols.ToArray());
                cols = new List<InlineKeyboardButton>();
                index++;

            }
            cols.Add(InlineKeyboardButton.WithCallbackData("Select to type", "Select to type"));
            rows.Add(cols.ToArray());
            return new InlineKeyboardMarkup(rows);
        }

        public static InlineKeyboardMarkup KeeBoardForType()
        {
            var rows = new List<InlineKeyboardButton[]>();
            var cols = new List<InlineKeyboardButton>();
            var allStudios = new Allstudios();
            var allPrice = StudioInfo.GetType(allStudios.ParseStudios("test.txt"));
            var index = 0;
            foreach (var priceForStudio in allPrice)
            {
                cols.Add(InlineKeyboardButton.WithCallbackData(priceForStudio, priceForStudio));

                if (index % 4 != 0) continue;
                rows.Add(cols.ToArray());
                cols = new List<InlineKeyboardButton>();
                index++;

            }
            cols.Add(InlineKeyboardButton.WithCallbackData("Select to style", "Select to style"));
            rows.Add(cols.ToArray());
            return new InlineKeyboardMarkup(rows);
        }

        public static InlineKeyboardMarkup KeeBoardForStyle()
        {
            var rows = new List<InlineKeyboardButton[]>();
            var cols = new List<InlineKeyboardButton>();
            var allStudios = new Allstudios();
            var allPrice = StudioInfo.GetStyle(allStudios.ParseStudios("test.txt"));
            var index = 0;
            foreach (var priceForStudio in allPrice)
            {
                cols.Add(InlineKeyboardButton.WithCallbackData(priceForStudio, priceForStudio));

                if (index % 4 != 0) continue;
                rows.Add(cols.ToArray());
                cols = new List<InlineKeyboardButton>();
                index++;

            }
            cols.Add(InlineKeyboardButton.WithCallbackData("Get studios", "Get studios"));
            rows.Add(cols.ToArray());
            return new InlineKeyboardMarkup(rows);
        }
    }
}
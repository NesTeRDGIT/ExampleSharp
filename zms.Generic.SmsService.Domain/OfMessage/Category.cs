﻿using zms.Common.SharedKernel.Base.Domain;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Категория сообщения
    /// </summary>
    public class Category : StaticReference<Category, string>
    {
        private Category(string value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Приглашение застрахованного лица на уточнение документа удостоверяющего личность
        /// </summary>
        public static Category Ins01 => new ("ins.01", "Приглашение застрахованного лица на уточнение документа удостоверяющего личность");

        /// <summary>
        /// Приглашение застрахованного лица на уточнение СНИЛС
        /// </summary>
        public static Category Ins02 => new ("ins.02", "Приглашение застрахованного лица на уточнение СНИЛС");

        /// <summary>
        /// Приглашение застрахованного лица на уточнение персональных данных
        /// </summary>
        public static Category Ins03 => new ("ins.03", "Приглашение застрахованного лица на уточнение персональных данных");

        /// <summary>
        /// Приглашение застрахованного лица на уточнение контактных данных
        /// </summary>
        public static Category Ins04 => new ("ins.04", "Приглашение застрахованного лица на уточнение контактных данных");

        /// <summary>
        /// Приглашение застрахованного лица на уточнение адреса
        /// </summary>
        public static Category Ins05 => new ("ins.05", "Приглашение застрахованного лица на уточнение адреса");

        /// <summary>
        /// Информирование застрахованного лица об изготовленном полисе
        /// </summary>
        public static Category Ins06 => new ("ins.06", "Информирование застрахованного лица об изготовленном полисе");

        /// <summary>
        /// Приглашение застрахованного лица на перевыпуск полиса ОМС
        /// </summary>
        public static Category Ins07 => new ("ins.07", "Приглашение застрахованного лица на перевыпуск полиса ОМС");

        /// <summary>
        /// Информирование застрахованного лица о сдаче полиса ОМС
        /// </summary>
        public static Category Ins08 => new ("ins.08", "Информирование застрахованного лица о сдаче полиса ОМС");

        /// <summary>
        /// Приглашение застрахованного лица на перестрахование
        /// </summary>
        public static Category Ins09 => new ("ins.09", "Приглашение застрахованного лица на перестрахование");

        /// <summary>
        /// Приглашение застрахованного лица на оформление полиса ребёнку
        /// </summary>
        public static Category Ins10 => new ("ins.10", "Приглашение застрахованного лица на оформление полиса ребёнку");

        /// <summary>
        /// Приглашение застрахованного лица на уточнение прикрепления
        /// </summary>
        public static Category Ins11 => new ("ins.11", "Приглашение застрахованного лица на уточнение прикрепления");

        /// <summary>
        /// Информирование застрахованного лица о возможности получения выписки полиса ОМС
        /// </summary>
        public static Category Ins12 => new ("ins.12", "Информирование застрахованного лица о возможности получения выписки полиса ОМС");

        /// <summary>
        /// Приглашение застрахованного лица на диспансеризацию
        /// </summary>
        public static Category Prev01 => new ("prev.01", "Приглашение застрахованного лица на диспансеризацию");

        /// <summary>
        /// Приглашение застрахованного лица на 2-этап диспансеризации
        /// </summary>
        public static Category Prev02 => new ("prev.02", "Приглашение застрахованного лица на 2-этап диспансеризации");

        /// <summary>
        /// Приглашение застрахованного лица на профилактическое мероприятие
        /// </summary>
        public static Category Prev03 => new ("prev.03", "Приглашение застрахованного лица на профилактическое мероприятие");

        /// <summary>
        /// Напоминание застрахованному лицу о необходимости диспансерного наблюдения
        /// </summary>
        public static Category Prev04 => new ("prev.04", "Напоминание застрахованному лицу о необходимости диспансерного наблюдения");

        /// <summary>
        /// Приглашение застрахованного лица на углублённую диспансеризацию
        /// </summary>
        public static Category Prev05 => new ("prev.05", "Приглашение застрахованного лица на углублённую диспансеризацию");
    }
}

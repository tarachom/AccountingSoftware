﻿/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     accounting.org.ua
*/

using System;

namespace AccountingSoftware
{
    /// <summary>
    /// Тригери-функції які запускаються перед записом, після запису або перед видаленням обєктів (довідник, документ і т.д) 
    /// </summary>
    public class ConfigurationTriggerFunctions
    {
        /// <summary>
        /// Перед записом
        /// </summary>
        public string BeforeSave { get; set; }

        /// <summary>
        /// Після запису
        /// </summary>
        public string AfterSave { get; set; }

        /// <summary>
        /// Перед видаленням
        /// </summary>
        public string BeforeDelete { get; set; }
    }
}

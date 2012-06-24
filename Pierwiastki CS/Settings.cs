using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;
using System.Globalization;

namespace NumericalCalculator
{
    [XmlRoot("settings")]
    public class Settings
    {
        SerializableDictionary<SettingEnum, object> settings;

        IsolatedStorageFile storage;
        IsolatedStorageFileStream stream;

        XmlSerializer serializer;

        private readonly string fileName = "Settings.xml";

        public object this[SettingEnum setting]
        {
            get
            {
                if (settings.ContainsKey(setting))
                    return settings[setting];
                else
                    throw new SettingNullReferenceException();
            }

            set
            {
                settings[setting] = value;

                Zapisz();
            }
        }

        private void Zapisz()
        {
            OpenStream();

            try
            {
                serializer = new XmlSerializer(typeof(SerializableDictionary<SettingEnum, object>));
                serializer.Serialize(stream, settings);
            }
            finally
            {
                CloseStream();
            }
        }

        private void Odczytaj()
        {
            OpenStream();

            try
            {
                serializer = new XmlSerializer(typeof(SerializableDictionary<SettingEnum, object>));
                settings = (SerializableDictionary<SettingEnum, object>)serializer.Deserialize(stream);
            }
            finally
            {
                CloseStream();
            }
        }

        public Settings()
        {
            settings = new SerializableDictionary<SettingEnum, object>();

            storage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

            ReadSettings();
        }

        private void ReadSettings()
        {
            //Czy plik istnieje
            if (storage.FileExists(fileName))
            {
                try
                {
                    //Deserializujemy
                    Odczytaj();
                }
                catch (Exception ex)
                {
                    //Ustawiamy defaultowe ustawienia
                    RestoreDefaults();
                }
            }
            else
            {
                //Ustawiamy defaultowe ustawienia
                RestoreDefaults();
            }
        }

        public void RestoreDefaults()
        {
            settings[SettingEnum.GraphMenuChecked] = true;
            settings[SettingEnum.GraphPreviewMenuChecked] = true;
            settings[SettingEnum.FunctionChecked] = true;
            settings[SettingEnum.FirstDerativeChecked] = false;
            settings[SettingEnum.SecondDerativeChecked] = false;
            settings[SettingEnum.DifferentialChecked] = false;
            settings[SettingEnum.DifferentialIIChecked] = false;
            settings[SettingEnum.SpecialFunctionChecked] = false;
            settings[SettingEnum.FourierTransformChecked] = false;
            settings[SettingEnum.InverseFourierTransformChecked] = false;
            settings[SettingEnum.AutomaticRescallingChecked] = true;

            switch (CultureInfo.CurrentCulture.Name)
            {
                case "pl-PL": settings[SettingEnum.Language] = LanguageEnum.Polish; break;
                default: settings[SettingEnum.Language] = LanguageEnum.English; break;
            }

            Zapisz();
        }

        private void OpenStream()
        {
            stream = storage.OpenFile(fileName, FileMode.OpenOrCreate);
        }

        private void CloseStream()
        {
            if (stream != null)
            {
                stream.Flush();
                stream.Close();
            }
        }
    }

    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");

                TKey key = (TKey)keySerializer.Deserialize(reader);

                reader.ReadEndElement();
                reader.ReadStartElement("value");

                TValue value = (TValue)valueSerializer.Deserialize(reader);

                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");

                keySerializer.Serialize(writer, key);

                writer.WriteEndElement();
                writer.WriteStartElement("value");

                TValue value = this[key];
                valueSerializer.Serialize(writer, value);

                writer.WriteEndElement();
                writer.WriteEndElement();
            }

            writer.Flush();
        }
    }

    public enum SettingEnum
	{
        GraphMenuChecked,
        GraphPreviewMenuChecked,
        FunctionChecked,
        FirstDerativeChecked,
        SecondDerativeChecked,
        DifferentialChecked,
        DifferentialIIChecked,
        SpecialFunctionChecked,
        FourierTransformChecked,
        InverseFourierTransformChecked,
        AutomaticRescallingChecked,
        Language
	}

    public class SettingNullReferenceException : Exception
    {
        public SettingNullReferenceException()
            : base("Brak ustawienia!")
        { }

        public SettingNullReferenceException(string msg)
            : base(msg)
        { }
    }
}

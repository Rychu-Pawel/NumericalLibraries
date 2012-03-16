using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;

namespace NumericalCalculator
{
    [XmlRoot("settings")]
    public class Settings
    {
        SerializableDictionary<Setting, object> settings;

        IsolatedStorageFile storage;
        IsolatedStorageFileStream stream;

        XmlSerializer serializer;

        private readonly string fileName = "Settings.xml";

        public object this[Setting setting]
        {
            get
            {
                if (settings.ContainsKey(setting))
                    return settings[setting];
                else
                    throw new BrakUstawieniaException();
            }

            set
            {
                settings[setting] = value;

                Zapisz();
            }
        }

        private void Zapisz()
        {
            if (stream != null)
                stream.Close();
            
            stream = storage.OpenFile(fileName, FileMode.Create);

            serializer = new XmlSerializer(typeof(SerializableDictionary<Setting, object>));
            serializer.Serialize(stream, settings);
        }

        private void Odczytaj()
        {
            serializer = new XmlSerializer(typeof(SerializableDictionary<Setting, object>));
            settings = (SerializableDictionary<Setting, object>)serializer.Deserialize(stream);
        }

        public Settings()
        {
            settings = new SerializableDictionary<Setting, object>();

            storage = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
            
            ReadSettings();
        }

        private void ReadSettings()
        {
            //Czy plik istnieje
            if (storage.FileExists(fileName))
            {
                //Pobieramy stream
                stream = storage.OpenFile(fileName, FileMode.Open);

                try
                {
                    //Deserializujemy
                    Odczytaj();
                }
                catch
                {
                    //Ustawiamy defaultowe ustawienia
                    PrzywrocUstawieniaDomyslne();
                }
            }
            else
            {
                //Ustawiamy defaultowe ustawienia
                PrzywrocUstawieniaDomyslne();
            }
        }

        public void PrzywrocUstawieniaDomyslne()
        {
            settings[Setting.WykresMenuChecked] = true;
            settings[Setting.PodgladWykresuMenuChecked] = true;
            settings[Setting.FunkcjaChecked] = true;
            settings[Setting.PierwszaPochodnaChecked] = false;
            settings[Setting.DrugaPochodnaChecked] = false;
            settings[Setting.RozniczkaChecked] = false;
            settings[Setting.RozniczkaIIChecked] = false;
            settings[Setting.EnergiaChecked] = false;
            settings[Setting.FunkcjaSpecjalnaChecked] = false;
            settings[Setting.FFTChecked] = false;
            settings[Setting.IFFTChecked] = false;
            settings[Setting.AutomatycznyReskallingChecked] = true;

            Zapisz();
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

    public enum Setting
	{
        WykresMenuChecked,
        PodgladWykresuMenuChecked,
        FunkcjaChecked,
        PierwszaPochodnaChecked,
        DrugaPochodnaChecked,
        RozniczkaChecked,
        RozniczkaIIChecked,
        EnergiaChecked,
        FunkcjaSpecjalnaChecked,
        FFTChecked,
        IFFTChecked,
        AutomatycznyReskallingChecked
	}

    public class BrakUstawieniaException : Exception
    {
        public BrakUstawieniaException()
            : base("Brak ustawienia!")
        { }

        public BrakUstawieniaException(string msg)
            : base(msg)
        { }
    }
}

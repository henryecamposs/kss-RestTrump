using ksslib;
using Newtonsoft.Json;
using RestTrump;
using System;
using System.Collections;
using System.Data;
using System.Xml;

namespace RestTrump
{
    public class FeedInstagram
    {
        #region ATRIBUTOS

        string __typename;
        string edge_media_to_caption;
        string shortcode;
        string edge_media_to_comment;
        string comments_disabled;
        string taken_at_timestamp;
        /*  <height>750</height>
            <width>750</width>
         */
        string dimensions;
        string display_url;
        string edge_liked_by;
        string edge_media_preview_like;
        string location;
        string gating_info;
        string fact_check_overall_rating;
        string fact_check_information;
        string media_preview;
        /*  <id>11102295773</id>
         *  <username>monitor.dolar.ve</username>
         */
        string owner;
        string thumbnail_src;
        /*  <src>dir url</src>
            <config_width>150</config_width>
            <config_height>150</config_height>
        */
        string thumbnail_resources;
        string is_video;
        string accessibility_caption;

        public string Typename { get => __typename; set => __typename = value; }
        public string Edge_media_to_caption { get => edge_media_to_caption; set => edge_media_to_caption = value; }
        public string Shortcode { get => shortcode; set => shortcode = value; }
        public string Edge_media_to_comment { get => edge_media_to_comment; set => edge_media_to_comment = value; }
        public string Comments_disabled { get => comments_disabled; set => comments_disabled = value; }
        public string Taken_at_timestamp { get => taken_at_timestamp; set => taken_at_timestamp = value; }
        public string Dimensions { get => dimensions; set => dimensions = value; }
        public string Display_url { get => display_url; set => display_url = value; }
        public string Edge_liked_by { get => edge_liked_by; set => edge_liked_by = value; }
        public string Edge_media_preview_like { get => edge_media_preview_like; set => edge_media_preview_like = value; }
        public string Location { get => location; set => location = value; }
        public string Gating_info { get => gating_info; set => gating_info = value; }
        public string Fact_check_overall_rating { get => fact_check_overall_rating; set => fact_check_overall_rating = value; }
        public string Fact_check_information { get => fact_check_information; set => fact_check_information = value; }
        public string Media_preview { get => media_preview; set => media_preview = value; }
        public string Owner { get => owner; set => owner = value; }
        public string Thumbnail_src { get => thumbnail_src; set => thumbnail_src = value; }
        public string Thumbnail_resources { get => thumbnail_resources; set => thumbnail_resources = value; }
        public string Is_video { get => is_video; set => is_video = value; }
        public string Accessibility_caption { get => accessibility_caption; set => accessibility_caption = value; }
        public FeedInstagram_Collection FeedsCollection { get; private set; }
        #endregion

        public FeedInstagram()
        {

        }
        public FeedInstagram(string jsonString, XmlReadMode mode = XmlReadMode.Auto)
        {
            jsonString = "{ \"rootNode\": {" + jsonString.Trim().TrimStart('{').TrimEnd('}') + @"} }";
            var xd = JsonConvert.DeserializeXmlNode(jsonString);

            XmlNodeList xnList = xd.SelectNodes("/rootNode/graphql/user/edge_owner_to_timeline_media/edges");
            foreach (XmlNode xn in xnList)
            {
                XmlNodeList xnn = xn.SelectNodes("node");
                try
                {
                    foreach (XmlNode item in xnn)
                    {
                        FeedInstagram feed = new FeedInstagram();
                        feed.Typename = item["__typename"].InnerText == null ? "" : item["__typename"].InnerText;
                        string str = item["edge_media_to_caption"].InnerText == null ? "" : item["edge_media_to_caption"].InnerText;
                        str = str.Replace("\n", ", ");
                        feed.Edge_media_to_caption = str;
                        feed.Shortcode = item["shortcode"].InnerText == null ? "" : item["shortcode"].InnerText;
                        feed.Edge_media_to_comment = item["edge_media_to_comment"].InnerText == null ? "" : item["edge_media_to_comment"].InnerText;
                        feed.Comments_disabled = item["comments_disabled"].InnerText == null ? "" : item["comments_disabled"].InnerText;
                        feed.Taken_at_timestamp = item["taken_at_timestamp"].InnerText == null ? "" : item["taken_at_timestamp"].InnerText;
                        feed.Dimensions = item["dimensions"].InnerText == null ? "" : item["dimensions"].InnerText;
                        feed.Display_url = item["display_url"].InnerText == null ? "" : item["display_url"].InnerText;
                        feed.Edge_liked_by = item["edge_liked_by"].InnerText == null ? "" : item["edge_liked_by"].InnerText;
                        feed.Edge_media_preview_like = item["edge_media_preview_like"].InnerText == null ? "" : item["edge_media_preview_like"].InnerText;
                        feed.Location = item["location"].InnerText == null ? "" : item["location"].InnerText;
                        feed.Gating_info = item["gating_info"].InnerText == null ? "" : item["gating_info"].InnerText;
                        feed.Fact_check_overall_rating = item["fact_check_overall_rating"].InnerText == null ? "" : item["fact_check_overall_rating"].InnerText;
                        feed.Fact_check_information = item["fact_check_information"].InnerText == null ? "" : item["fact_check_information"].InnerText;
                        feed.Media_preview = item["media_preview"].InnerText == null ? "" : item["media_preview"].InnerText;
                        feed.Owner = item["owner"].InnerText == null ? "" : item["owner"].InnerText;
                        feed.Thumbnail_src = item["thumbnail_src"].InnerText == null ? "" : item["thumbnail_src"].InnerText;
                        feed.Thumbnail_resources = item["thumbnail_resources"].InnerText == null ? "" : item["thumbnail_resources"].InnerText;
                        feed.Is_video = item["is_video"].InnerText == null ? "" : item["is_video"].InnerText;
                        feed.Accessibility_caption = item["accessibility_caption"].InnerText == null ? "" : item["accessibility_caption"].InnerText;
                        addFeed(feed);
                    }
                }
                catch (Exception ex)
                {
                    clsUtilErrors.Manejador_errores(ex, false);
                }
            }
        }

        private void addFeed(FeedInstagram feed)
        {

            if (FeedsCollection == null)
                FeedsCollection = new FeedInstagram_Collection(this.GetType().FullName);
            FeedsCollection.Add(feed);
        }
    }
}

/// <summary>
/// Coleccion de Facturas
/// </summary>
public class FeedInstagram_Collection : CollectionBase
{
    private String TipoObjeto;

    public FeedInstagram_Collection(string TipoObjeto)
    {
        this.TipoObjeto = TipoObjeto;
    }

    // Para acceder a los elementos mediante []
    public FeedInstagram this[int index]
    {
        get
        {
            return ((FeedInstagram)List[index]);
        }
        set
        {
            List[index] = value;
        }
    }

    public int Add(FeedInstagram value)
    {
        return (List.Add(value));
    }

    public int IndexOf(FeedInstagram value)
    {
        return (List.IndexOf(value));
    }

    public void Insert(int index, FeedInstagram value)
    {
        List.Insert(index, value);
    }

    public void Remove(FeedInstagram value)
    {
        List.Remove(value);
    }

    public bool Contains(FeedInstagram value)
    {
        return (List.Contains(value));
    }

    protected override void OnInsert(int index, Object value)
    {
        if (value.GetType() != Type.GetType(TipoObjeto))
            throw new ArgumentException("El objeto debe se de tipo FeedInstagram.", "value");
    }

    protected override void OnRemove(int index, Object value)
    {
        if (value.GetType() != Type.GetType(TipoObjeto))
            throw new ArgumentException("El objeto debe se de tipo FeedInstagram.", "value");
    }

    protected override void OnSet(int index, Object oldValue, Object newValue)
    {
        if (newValue.GetType() != Type.GetType(TipoObjeto))
            throw new ArgumentException("El Nuevo valor debe se de tipo FeedInstagram.", "newValue");
    }

    protected override void OnValidate(Object value)
    {
        if (value.GetType() != Type.GetType(TipoObjeto))
            throw new ArgumentException("El valor debe se de tipo FeedInstagram.");
    }
}


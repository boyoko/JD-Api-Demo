using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace JD.NetCore.Common
{
    public class NestHelper
    {


        private static ElasticClient _client;
        public static ElasticClient GetInstance(string defaultIndex)
        {
            var uris = new[]
            {
                new Uri("http://localhost:9200"),
                new Uri("http://47.93.121.161:9200"),
                //new Uri("http://localhost:9201"),
                //new Uri("http://localhost:9202"),
            };

            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool)
                .DefaultIndex(defaultIndex);

            _client = new ElasticClient(settings);

            return _client;
        }


        public static IIndexResponse Index<T>(T t)
        {
            var x = t as IIndexRequest;
            var indexResponse = _client.Index(x);
            return indexResponse;
        }

        //public ISearchResponse<T> Search()
        //{
        //    var searchResponse = _client.Search<T>(s => s
        //                            .From(0)
        //                            .Size(10)
        //                            .Query(q => q
        //                                 .Match(m => m
        //                                    .Field(f => f.FirstName)
        //                                    .Query("Martijn")
        //                                 )
        //                            )
        //                        );

        //    var t = searchResponse.Documents;

        //    return t;
        //}


    }
}

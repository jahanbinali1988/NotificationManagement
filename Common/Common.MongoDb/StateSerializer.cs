using System;
using System.Collections.Generic;
using System.Text;
using Common.Domain.Utilities;
//using MongoDB.Bson.Serialization;
//using MongoDB.Bson.Serialization.Serializers;

namespace Common.MongoDb
{
    //public class StateSerializer<T> : SerializerBase<T> where T : class
    //{
    //    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
    //    {
    //        context.Writer.WriteInt64(new StateFactory<T>().Create(value));
    //    }

    //    public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    //    {
    //        return new StateFactory<T>().Create(context.Reader.ReadInt64());
    //    }
    //}
}

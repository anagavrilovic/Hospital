using Hospital.Model;
using Newtonsoft.Json;
using System;

namespace Hospital
{
    public class Room 
    {
        public Room()
        {
            SerializeInfo = true;
        }

        public string Id { get; set; }
        public String Name { get; set; }

        public int Floor { get; set; }
        public RoomStatus Status { get; set; }

        public RoomType Type { get; set; }
        public int FreeBeds { get; set; }

        public bool ShouldSerializeName()
        {
            return this.SerializeInfo;
        }

        public bool ShouldSerializeFloor()
        {
            return this.SerializeInfo;
        }

        public bool ShouldSerializeStatus()
        {
            return this.SerializeInfo;
        }

        public bool ShouldSerializeType()
        {
            return this.SerializeInfo;       
        }

        public bool ShouldSerializeFreeBeds()
        {
            return this.SerializeInfo;
        }

        [JsonIgnore]
        public bool SerializeInfo { get; set; }

        override
        public string ToString()
        {
            return this.Name;
        }

        public bool IsMagazine()
        {
            return this.Type.Equals(RoomType.MAGACIN);
        }

        public bool IsSuitableRoom(AppointmentType appointmentType)
        {
            if (appointmentType.Equals(AppointmentType.examination) || appointmentType.Equals(AppointmentType.urgentExamination))
                if (this.Type.Equals(RoomType.SALA_ZA_PREGLEDE))
                    return true;

            if (appointmentType.Equals(AppointmentType.operation) || appointmentType.Equals(AppointmentType.urgentOperation))
                if (this.Type.Equals(RoomType.OPERACIONA_SALA))
                    return true;

            return false;
        }
    }
}
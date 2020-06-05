using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly ParkingContext db;
        SlotLimt limit = new SlotLimt();
        public ParkingRepository(ParkingContext _db)
        {
            db = _db;
        }

        public List<ParkingPortal> GetDetail()
        {
            return (from p in db.parkingPortals
                    select new ParkingPortal
                    {
                        ReceiptNo = p.ReceiptNo,
                        DriverName = p.DriverName,
                        VehicleColor = p.VehicleColor,
                        VehicleNumber = p.VehicleNumber,
                        Brand = p.Brand,
                        Slot = p.Slot,
                        Status=p.Status,
                        ParkingDate = p.ParkingDate
                    }).ToList();
        }

        public object GetVehicleByNo(string number)
        {
            ParkingPortal portal = new ParkingPortal();
            portal.VehicleNumber = number;
            try
            {
                if (db.parkingPortals.Any(p => p.VehicleNumber == number))
                {
                    return (from table in db.parkingPortals
                            where table.VehicleNumber == number
                            select table).ToList();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object GetVehicleByBrand(string name)
        {
            try
            {
                if (db.parkingPortals.Any(p => p.Brand == name))
                {
                    var data = (from table in db.parkingPortals
                                where table.Brand == name
                                select table).ToList();
                    return data;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object ParkStatus(ParkingStatus parkingStatus)
        {
            try
            {
                double total = db.parkingPortals.Where(p => p.ReceiptNo == parkingStatus.ReceiptNo)
                      .Select(i => (DateTime.Now.Subtract(i.ParkingDate).TotalMinutes)).Sum();

                parkingStatus.Date = DateTime.Now;
                parkingStatus.Charges =Convert.ToInt32( total * 10);

                var Status = db.parkingPortals.Find(parkingStatus.ReceiptNo);
                Status.Status = "UnPark";
                db.parkingPortals.Update(Status);
                db.Add(parkingStatus);
                db.SaveChanges();

                var data = (from p in db.parkingPortals
                            where p.ReceiptNo == parkingStatus.ReceiptNo
                            from q in db.ParkingStatuses
                            select new
                            {
                                p.ReceiptNo,
                                p.DriverName,
                                p.Brand,
                                p.VehicleColor,
                                p.Slot,
                                p.VehicleNumber,
                                p.ParkingDate,
                                parkingStatus.Date,
                                parkingStatus.Charges

                            }).FirstOrDefault();

                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object CountSlot()
        {
            return (from p in db.parkingPortals
                    where p.Status == "Parked"
                    select p
                    ).Count();
        }

        public string CheckSlot()
        {
            var condition = db.parkingPortals.Where(p => p.Slot == "A" && p.Status == "Parked").Count();
            var condition1 = db.parkingPortals.Where(p => p.Slot == "B" && p.Status == "Parked").Count();
            var condition2 = db.parkingPortals.Where(p => p.Slot == "C" && p.Status == "Parked").Count();
            var condition3 = db.parkingPortals.Where(p => p.Slot == "D" && p.Status == "Parked").Count();
            if (condition <= limit.A)
            {
                return "A";
            }
            else if (condition1 <= limit.B)
            {
                return "B";
            }
            else if (condition2 <= limit.C)
            {
                return "C";
            }
            else if(condition3<=limit.D)
            {
                return  "D";
            }
            else
            {
                throw new Exception("Parking Is Full");
            }
        }

        public ParkingPortal AddData(ParkingPortal parkingPortal)
        {
            if (parkingPortal.Handicap == "Yes")
            {
                parkingPortal.Slot = "A";
            }
            parkingPortal.Status = "Parked";
            parkingPortal.ParkingDate = DateTime.Now;
            parkingPortal.Slot = CheckSlot();
            db.Add(parkingPortal);
            db.SaveChanges();
            return parkingPortal;
        }
        }
    }
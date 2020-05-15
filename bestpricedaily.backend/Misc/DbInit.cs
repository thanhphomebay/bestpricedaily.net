using System;
using System.Linq;
using bestpricedaily.Models;

namespace bestpricedaily.Misc
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;
    
    public class DbInit
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataDbContext>>()))
            {
                // Look for any board games.
                if (context.Items.Any())
                {
                    return;   // Data was already seeded
                }

                context.Items.AddRange(

                    new Item
                    {
                        //sku =Guid.Parse( "7ceac2a2-2267-491d-89fe-a7109239fb33"),
                        price = 10,
                        name = "N95 NIOSH MASK WITH CE FDA 1",
                        des = "N95 respirators and surgical masks (face masks) are examples of personal protective equipment that are used to protect the wearer from airborne particles and from liquid contaminating the face. Centers for Disease Control and Prevention (CDC) National Institute for Occupational Safety and Health (NIOSH) and Occupational Safety and Health Administration (OSHA) also regulate N95 respirators.",
                        pix = "pix/medical/3mn95mask.jpg",
                        //quantityAvailable = 100,
                    },
                    new Item
                    {
                        //sku = Guid.Parse("374e0f55-fd6f-483d-94c1-abf52b125882"),
                        price = 1.50F,
                        name = "Disposalble Mask",
                        des = "N95 respirators and surgical masks (face masks) are examples of personal protective equipment that are used to protect the wearer from airborne particles and from liquid contaminating the face. Centers for Disease Control and Prevention (CDC) National Institute for Occupational Safety and Health (NIOSH) and Occupational Safety and Health Administration (OSHA) also regulate N95 respirators.",
                        pix = "pix/medical/disposalblemask.jpg",
                        //quantityAvailable = 100,
                    },
                    new Item
                    {
                        //sku = Guid.Parse("f35e26bb-691d-4d1f-9a35-ced50573c884"),
                        price = 0.75F,
                        name = "Dusk Mask",
                        des = "N95 respirators and surgical masks (face masks) are examples of personal protective equipment that are used to protect the wearer from airborne particles and from liquid contaminating the face. Centers for Disease Control and Prevention (CDC) National Institute for Occupational Safety and Health (NIOSH) and Occupational Safety and Health Administration (OSHA) also regulate N95 respirators.",
                        pix = "pix/medical/duskmask.jpg",
                        //quantityAvailable = 100,
                    },
                    new Item
                    {
                        //sku = Guid.Parse("fca4297a-9de9-4161-b1da-7178f79225c2"),
                        price = 30,
                        name = "Gas Mask",
                        des = "N95 respirators and surgical masks (face masks) are examples of personal protective equipment that are used to protect the wearer from airborne particles and from liquid contaminating the face. Centers for Disease Control and Prevention (CDC) National Institute for Occupational Safety and Health (NIOSH) and Occupational Safety and Health Administration (OSHA) also regulate N95 respirators.",
                        pix = "pix/medical/gasmask.jpg",
                        //quantityAvailable = 100,
                    });

                context.SaveChanges();
            }
        }
    }
}

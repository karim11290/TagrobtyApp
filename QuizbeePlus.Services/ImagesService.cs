using QuizbeePlus.Data;
using QuizbeePlus.Entities;
using QuizbeePlus.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Services
{
    public class ImagesService
    {
        #region Define as Singleton
        private static ImagesService _Instance;

        public static ImagesService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ImagesService();
                }

                return (_Instance);
            }
        }

        private ImagesService()
        {
        }
        #endregion
        
        public bool SaveNewImage(Image image)
        {
            using (var context = new QuizbeeContext())
            {
                context.Images.Add(image);

                return context.SaveChanges() > 0;
            }
        }

        public Image GetImage(int ID)
        {
            using (var context = new QuizbeeContext())
            {
                return context.Images
                                    .Where(q => q.ID == ID)
                                    .FirstOrDefault();
            }                        
        }
    }
}

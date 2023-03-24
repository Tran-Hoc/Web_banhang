using Microsoft.EntityFrameworkCore;
using Web_banhang.Data;
using Web_banhang.DataContext;
using Web_banhang.Models;

namespace Web_banhang.Service
{
    public class NewsRepository : INewsRepository<VMNews>
    {
        WebBanHangContext _context;

        public NewsRepository(WebBanHangContext context)
        {
            _context = context;
        }

       

        public async Task<List<VMNews>> GetAll()
        {
            var listNews = await _context.TbNews.ToListAsync();

            List<VMNews> result = new List<VMNews>();

            foreach (var item in listNews)
            {
                VMNews vMNews = new VMNews
                {
                    Id = item.Id,
                    Title = item.Title,
                    Image = "~/files/Image/" + item.Image,
                    CreatedDate = item.CreatedDate,
                };
                result.Add(vMNews);
            }

            return result;
        }

        public async Task<VMNews> GetById(int? id)
        {

            if (id == null || _context.TbNews == null)
            {
                return null;
            }

            var news = await _context.TbNews.FirstOrDefaultAsync(m => m.Id == id);

            if (news == null)
            {
                return null;
            }
            VMNews vMNews = new VMNews
            {
                Id = news.Id,
                Title = news.Title,
                Image = news.Image,
                CreatedDate = news.CreatedDate,
                Description = news.Description,
                Detail = news.Detail,
                SeoDescription = news.SeoDescription,
                SeoKeywords = news.SeoKeywords,
                SeoTitle = news.SeoTitle,
            };
            return vMNews;

        }
        public async Task<bool> AddAsync(VMNews t)
        {
            if (_context.TbNews != null)
            {
                TbNews tbNews = new TbNews
                {
                    Title = t.Title,
                    Description = t.Description,
                    Detail = t.Detail,
                    Image = t.Image,
                    SeoDescription = t.SeoDescription,
                    SeoTitle = t.SeoTitle,
                    CategoryId = 14,
                    SeoKeywords = t.SeoKeywords,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Alias = Web_banhang.Models.Filter.FilterChar(t.Title),

                };
                //= SaveFile(t.FileImage, tbNews.CreatedDate.ToString("MM-dd-yyyy-h-mm-tt"), 1);

                try
                {
                    await _context.AddAsync(tbNews);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        public async Task<bool> Update(VMNews t)
        {
            if (_context.TbNews != null)
            {
                TbNews tbNews = new TbNews
                {
                    Id = t.Id ?? 1,
                    Title = t.Title,
                    Description = t.Description,
                    Detail = t.Detail,
                    Image = t.Image,
                    SeoDescription = t.SeoDescription,
                    SeoTitle = t.SeoTitle,
                    CategoryId = 14,
                    SeoKeywords = t.SeoKeywords,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Alias = Web_banhang.Models.Filter.FilterChar(t.Title),

                };
                //= SaveFile(t.FileImage, tbNews.CreatedDate.ToString("MM-dd-yyyy-h-mm-tt"), 1);

                try
                {
                     //_context.Entry(tbNews).State = EntityState.Modified;
                    await _context.AddAsync(tbNews);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            var result = _context.TbNews.Where(e => e.Id == id).FirstOrDefault();
            if (result != null)
            {
                _context.TbNews.Remove(result);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
   
        public Task<VMNews> UpdateById(int? id)
        {
            throw new NotImplementedException();
        }

        public int IsActive(int id)
        {
            var item = _context.TbNews.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.SaveChanges();
                return item.IsActive ? 1 : 2; // true 1 : false 2
            }
            return 0;
        }

        private string SaveFile(IFormFile file, string id, int typ)
        {
            try
            {
                string path = "";
                if (typ == 1)
                {
                    string imgpath = "wwwroot\\files\\Image";
                    path = Path.Combine(Directory.GetCurrentDirectory(), imgpath);
                }
                else
                {
                    string videopath = "wwwroot\\files\\Video";
                    path = Path.Combine(Directory.GetCurrentDirectory(), videopath);

                }

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //get file extension
                FileInfo fileInfo = new FileInfo(file.FileName);
                string fileName = id + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);
                if (File.Exists(fileNameWithPath))
                {
                    File.Delete(fileNameWithPath);
                }

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

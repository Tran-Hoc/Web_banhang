using Microsoft.EntityFrameworkCore;
using Web_banhang.Data;
using Web_banhang.DataContext;
using Web_banhang.Models;

namespace Web_banhang.Service
{
    public class NewsRepository : IRepository<NewsVM>
    {
        WebBanHangContext _context;

        public NewsRepository(WebBanHangContext context)
        {
            _context = context;
        }

        public async Task<List<NewsVM>> GetAllAsync()
        {
            var listNews = await _context.TbNews.OrderByDescending(x => x.Id).ToListAsync();

            List<NewsVM> result = new List<NewsVM>();

            foreach (var item in listNews)
            {
                NewsVM vMNews = new NewsVM
                {
                    Id = item.Id,
                    Title = item.Title,
                    Image = "~/files/Image/" + item.Image,
                    CreatedDate = item.CreatedDate,
                    IsActive = item.IsActive,
                };
                result.Add(vMNews);
            }

            return result;
        }

        public Task<List<NewsVM>> GetPageListAsync(int? page)
        {
            //var listNews = await _context.TbNews.ToListAsync();

            //List<NewsVM> result = new List<NewsVM>();

            //if (page == null) page = 1;
            //var links = (from l in _context.TbNews
            //             select l).OrderBy(x => x.Id);
            //int pagesize = 5;
            //int pageNumber = (page ?? 1);

            ////return View(links.ToPagedList(pageNumber, pagesize));

            //foreach (var item in listNews)
            //{
            //    NewsVM vMNews = new NewsVM
            //    {
            //        Id = item.Id,
            //        Title = item.Title,
            //        Image = "~/files/Image/" + item.Image,
            //        CreatedDate = item.CreatedDate,
            //        IsActive = item.IsActive,
            //    };
            //    result.Add(vMNews);
            //}

            //return result;
            throw new NotImplementedException();
        }

        public async Task<List<NewsVM>> GetSearchAsync(string searchstring)
        {
            List<TbNews> listNews = await _context.TbNews.Where( x => x.Title.Contains(searchstring) || x.Alias.Contains(searchstring)).ToListAsync();
            //listNews = (List<TbNews>) listNews.Where(x => x.Title.Contains(searchstring) || x.Alias.Contains(searchstring));

            //List<VMNews> result = new List<VMNews>();

            //if (page == null) page = 1;
            //var links = (from l in _context.TbNews
            //             select l).OrderBy(x => x.Id);
            //int pagesize = 5;
            //int pageNumber = (page ?? 1);

            //return View(links.ToPagedList(pageNumber, pagesize));

            List<NewsVM> result = new List<NewsVM>();

            foreach (var item in listNews)
            {
                NewsVM vMNews = new NewsVM
                {
                    Id = item.Id,
                    Title = item.Title,
                    Image = "~/files/Image/" + item.Image,
                    CreatedDate = item.CreatedDate,
                    IsActive = item.IsActive,
                };
                result.Add(vMNews);
            }

            return result;
        }

        public async Task<NewsVM?> GetById(int? id)
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
            NewsVM vMNews = new NewsVM
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
                IsActive = news.IsActive,
            };
            return vMNews;

        }
        public async Task<bool> CreateAsync(NewsVM t)
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
                    IsActive = t.IsActive,
                    Alias = Filter.FilterChar(t.Title),

                };
                //= SaveFile(t.FileImage, tbNews.CreatedDate.ToString("MM-dd-yyyy-h-mm-tt"), 1);

                try
                {
                    await _context.AddAsync(tbNews);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
        public async Task<bool> UpdateAsync(NewsVM t)
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
                    Alias = Filter.FilterChar(t.Title),

                };
                //= SaveFile(t.FileImage, tbNews.CreatedDate.ToString("MM-dd-yyyy-h-mm-tt"), 1);

                try
                {
                    _context.Entry(tbNews).State = EntityState.Modified;
                    //await _context.AddAsync(tbNews);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = _context.TbNews.Where(e => e.Id == id).FirstOrDefault();
            if (result != null)
            {
                _context.TbNews.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<NewsVM> UpdateByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public int IsActive(int id)
        {
            var item = _context.TbNews.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return item.IsActive ? 1 : 2; // true 1 : false 2
            }
            return 0;
        }

        private string? SaveFile(IFormFile file, string id, int typ)
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
            catch (Exception)
            {
                return null;
            }
        }
    }
}

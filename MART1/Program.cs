// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");









namespace ProgrammingLanguagesApp
{
    // Programlama Dili varlık sınıfı
    public class ProgrammingLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Teknoloji varlık sınıfı
    public class Technology
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
    }

    // Programlama dili için repository arayüzü
    public interface IProgrammingLanguageRepository
    {
        void Add(ProgrammingLanguage language);
        List<ProgrammingLanguage> GetAll();
        void Delete(int id);
        void Update(ProgrammingLanguage language);
        ProgrammingLanguage GetById(int id);
    }

    // Teknoloji için repository arayüzü
    public interface ITechnologyRepository
    {
        void Add(Technology technology);
        List<Technology> GetAll();
        void Delete(int id);
        void Update(Technology technology);
        Technology GetById(int id);
    }

    // Programlama dili repository
    public class ProgrammingLanguageRepository : IProgrammingLanguageRepository
    {
        private List<ProgrammingLanguage> _languages = new List<ProgrammingLanguage>();

        public void Add(ProgrammingLanguage language)
        {
            _languages.Add(language);
        }

        public List<ProgrammingLanguage> GetAll()
        {
            return _languages;
        }

        public void Delete(int id)
        {
            var language = _languages.FirstOrDefault(l => l.Id == id);
            if (language != null) _languages.Remove(language);
        }

        public void Update(ProgrammingLanguage language)
        {
            var lang = _languages.FirstOrDefault(l => l.Id == language.Id);
            if (lang != null)
            {
                lang.Name = language.Name;
            }
        }

        public ProgrammingLanguage GetById(int id)
        {
            return _languages.FirstOrDefault(l => l.Id == id);
        }
    }

    // Teknoloji repository
    public class TechnologyRepository : ITechnologyRepository
    {
        private List<Technology> _technologies = new List<Technology>();

        public void Add(Technology technology)
        {
            _technologies.Add(technology);
        }

        public List<Technology> GetAll()
        {
            return _technologies;
        }

        public void Delete(int id)
        {
            var technology = _technologies.FirstOrDefault(t => t.Id == id);
            if (technology != null) _technologies.Remove(technology);
        }

        public void Update(Technology technology)
        {
            var tech = _technologies.FirstOrDefault(t => t.Id == technology.Id);
            if (tech != null)
            {
                tech.Name = technology.Name;
                tech.ProgrammingLanguage = technology.ProgrammingLanguage;
            }
        }

        public Technology GetById(int id)
        {
            return _technologies.FirstOrDefault(t => t.Id == id);
        }
    }

    // Programlama dili iş mantığı
    public interface IProgrammingLanguageService
    {
        void Add(ProgrammingLanguage language);
        List<ProgrammingLanguage> GetAll();
        void Delete(int id);
        void Update(ProgrammingLanguage language);
        ProgrammingLanguage GetById(int id);
    }

    public class ProgrammingLanguageManager : IProgrammingLanguageService
    {
        private IProgrammingLanguageRepository _repository;

        public ProgrammingLanguageManager(IProgrammingLanguageRepository repository)
        {
            _repository = repository;
        }

        public void Add(ProgrammingLanguage language)
        {
            _repository.Add(language);
        }

        public List<ProgrammingLanguage> GetAll()
        {
            return _repository.GetAll();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Update(ProgrammingLanguage language)
        {
            _repository.Update(language);
        }

        public ProgrammingLanguage GetById(int id)
        {
            return _repository.GetById(id);
        }
    }

    // Teknoloji iş mantığı
    public interface ITechnologyService
    {
        void Add(Technology technology);
        List<Technology> GetAll();
        void Delete(int id);
        void Update(Technology technology);
        Technology GetById(int id);
    }

    public class TechnologyManager : ITechnologyService
    {
        private ITechnologyRepository _repository;

        public TechnologyManager(ITechnologyRepository repository)
        {
            _repository = repository;
        }

        public void Add(Technology technology)
        {
            _repository.Add(technology);
        }

        public List<Technology> GetAll()
        {
            return _repository.GetAll();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Update(Technology technology)
        {
            _repository.Update(technology);
        }

        public Technology GetById(int id)
        {
            return _repository.GetById(id);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IProgrammingLanguageRepository languageRepository = new ProgrammingLanguageRepository();
            IProgrammingLanguageService languageService = new ProgrammingLanguageManager(languageRepository);

            ITechnologyRepository technologyRepository = new TechnologyRepository();
            ITechnologyService technologyService = new TechnologyManager(technologyRepository);

            while (true)
            {
                Console.WriteLine("1- Yeni Programlama Dili Ekle");
                Console.WriteLine("2- Programlama Dillerini Listele");
                Console.WriteLine("3- Yeni Teknoloji Ekle");
                Console.WriteLine("4- Teknolojileri Listele");
                Console.WriteLine("5- Çıkış");
                var secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        Console.WriteLine("Programlama dili adını giriniz:");
                        var name = Console.ReadLine();
                        languageService.Add(new ProgrammingLanguage { Id = new Random().Next(1, 1000), Name = name });
                        break;
                    case "2":
                        var languages = languageService.GetAll();
                        foreach (var lang in languages)
                        {
                            Console.WriteLine($"ID: {lang.Id}, Name: {lang.Name}");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Teknoloji adını giriniz:");
                        var techName = Console.ReadLine();
                        Console.WriteLine("Programlama dili ID'sini giriniz:");
                        var languageId = int.Parse(Console.ReadLine());
                        var language = languageService.GetById(languageId);
                        if (language != null)
                        {
                            technologyService.Add(new Technology { Id = new Random().Next(1, 1000), Name = techName, ProgrammingLanguage = language });
                        }
                        else
                        {
                            Console.WriteLine("Programlama dili bulunamadı!");
                        }
                        break;
                    case "4":
                        var technologies = technologyService.GetAll();
                        foreach (var tech in technologies)
                        {
                            Console.WriteLine($"ID: {tech.Id}, Name: {tech.Name}, Language: {tech.ProgrammingLanguage.Name}");
                        }
                        break;
                    case "5":
                        return;
                }
            }
        }
    }
}
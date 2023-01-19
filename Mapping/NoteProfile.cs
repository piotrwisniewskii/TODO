using AutoMapper;
using TODO.Data.Models;
using TODO.Data.Models.ViewModels;

namespace TODO.Mapping
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteVM>();
            CreateMap<NoteVM, Note>();
            CreateMap<TODOUser, TODOUserVM>();
            CreateMap<TODOUserVM, TODOUser>();
        }
    }
}

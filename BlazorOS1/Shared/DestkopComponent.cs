using BlazorOS1.Models;
using BlazorOS1.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorOS1
{
    public class DestkopComponent : ComponentBase
    {
        [Parameter] public List<IconModel> Icons { get; set; }

        [Parameter] public IJSRuntime jsRuntime { get; set; }
        public bool isFileManager { get; set; }
        public IconModel selectedIcon { get; set; }




        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            base.BuildRenderTree(builder);

            var seq = 0;
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "desktop");

            //foreach (var icon in Icons)
            //{
            //    if (icon.Type == FileTypes.sys)
            //    {
            //        builder.OpenElement(seq++, "div");
            //        builder.AddAttribute(seq++, "class", "icon filemanagerIcon");
            //        builder.AddAttribute(seq++, "style", $"background-image: url('../assets/{icon.Picture}');");
            //        builder.AddAttribute(seq++, "draggable", "true");
            //        builder.AddAttribute(seq++, "ondblclick", EventCallback.Factory.Create<MouseEventArgs>(this, () => FileManagerClickInternal()));
            //        builder.CloseElement();

            //    }
            //}
            
            builder.OpenRegion(seq++);
            var subseq = 0;
            foreach (IconModel icon in Icons)
            {
                if (icon.Type == FileTypes.sys && icon.IdContainer==0)
                {
                    builder.OpenComponent<Icon>(subseq++);
                    builder.AddAttribute(subseq++, "IconModel", icon);
                    builder.AddAttribute(subseq++, "OnClickIcon", EventCallback.Factory.Create<int>(this, () => IconClickInternal(icon.Id)));
                    builder.AddAttribute(subseq++, "OnDragStart", EventCallback.Factory.Create<int>(this, () => DragStartInternal(icon.Id)));
                    builder.CloseComponent();

                }
            }
            builder.CloseRegion();


            if (isFileManager)
            {
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "folder");
               
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "titleBar");
                builder.AddAttribute(seq++, "draggable", "true");

                    builder.OpenElement(seq++, "div");
                    builder.AddAttribute(seq++, "class", "closeButton");
                    builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create<int>(this, () => IconClickInternal(1)));
                    builder.CloseElement();

                    builder.OpenElement(seq++, "div");
                    builder.AddAttribute(seq++, "class", "nameTitle");
                    builder.AddContent(seq++, "Main");
                    builder.CloseElement();

                builder.CloseElement(); //titleBar


                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "folderContent");
                builder.AddAttribute(seq++, "ondragover", "event.preventDefault();");
                builder.AddAttribute(seq++, "ondrop", EventCallback.Factory.Create<DragEventArgs>(this, () => HandleDayOnDrop(1)));


                foreach (IconModel icon in Icons)
                {
                    if (icon.IdContainer == 1)
                    {
                        builder.OpenComponent<Icon>(seq++);
                        builder.AddAttribute(seq++, "IconModel", icon);
                        builder.AddAttribute(seq++, "OnDblClickIcon", EventCallback.Factory.Create<IconModel>(this, () => IconDblClickInternal(icon)));
                        builder.CloseComponent();

                    }
                }
                builder.CloseElement();
                

                builder.CloseElement();
            }


            builder.CloseElement();

        }


        private void IconClickInternal(int id)
        {
            isFileManager = !isFileManager;
        }

        private void DragStartInternal(int id)
        {
            selectedIcon = Icons.First(i => i.Id == id);
        }

        private async Task IconDblClickInternal(IconModel icon)
        {
            if (icon.Type == FileTypes.mp3)
            {
                await jsRuntime.InvokeAsync<string>("PlayFile", "../assets/win31.mp3");
            }
        }

        private async Task HandleDayOnDrop(int IdDiv)
        {
            if (selectedIcon != null)
            {
                selectedIcon.IdContainer = IdDiv;
            }
        }

        
        
    }
}

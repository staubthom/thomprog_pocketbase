
export function bg_color(color)
{
  var sel = "";
  var range = "";

  //Change Textcolor

  // Get Selection
  sel = window.getSelection();
  if (sel.rangeCount && sel.getRangeAt)
  {
    range = sel.getRangeAt(0);
  }
  // Set design mode to on
  document.designMode = "on";
  if (range)
  {
    sel.removeAllRanges();
    sel.addRange(range);
  }
  // Colorize text
  document.execCommand("backColor", false, color);
  // Set design mode to off
  document.designMode = "off";

}


export function text_color(color)
{
  var sel = "";
  var range = "";

  //Change Textcolor

  // Get Selection
  sel = window.getSelection();
  if (sel.rangeCount && sel.getRangeAt)
  {
    range = sel.getRangeAt(0);
  }
  // Set design mode to on
  document.designMode = "on";
  if (range)
  {
    sel.removeAllRanges();
    sel.addRange(range);
  }
  // Colorize text
  document.execCommand("ForeColor", false, color);
  // Set design mode to off
  document.designMode = "off";

}


export function clear()
{
  var sel = "";
  var range = "";

  //Change Textcolor

  // Get Selection
  sel = window.getSelection();
  if (sel.rangeCount && sel.getRangeAt)
  {
    range = sel.getRangeAt(0);
  }
  // Set design mode to on
  document.designMode = "on";
  if (range)
  {
    sel.removeAllRanges();
    sel.addRange(range);
  }
  // Colorize text
  document.execCommand("removeFormat", false, null);
  // Set design mode to off
  document.designMode = "off";
}

export function getEditor()
{
  var inhalt = document.getElementById("editor").innerHTML;

  return inhalt;
}
export function addNote()
{

  var sel, range;
  if (window.getSelection)
  {
    // IE9 and non-IE
    sel = window.getSelection();
    if (sel.getRangeAt && sel.rangeCount)
    {
      range = sel.getRangeAt(0);
      //range.deleteContents();
      // Range.createContextualFragment() would be useful here but is
      // only relatively recently standardized and is not supported in
      // some browsers (IE9, for one)
      var el = document.createElement("div");
      var Note = prompt("Enter your Note : ", "");
      var html = '<div class="myNote"> &#9757;</div> <div class="hide">' + Note + '</div>';
      el.innerHTML = html;
      var frag = document.createDocumentFragment(), node, lastNode;
      while ((node = el.firstChild))
      {
        lastNode = frag.appendChild(node);
      }
      range.insertNode(frag);

      // Preserve the selection
      if (lastNode)
      {
        range = range.cloneRange();
        range.setStartAfter(lastNode);
        range.collapse(true);
        sel.removeAllRanges();
        sel.addRange(range);
      }
    }
  }

}



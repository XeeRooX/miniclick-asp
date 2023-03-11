async function main() {
    $("#cut-button").click(GenerateHandler);
    $("#copy-link").click(CopyHandler);
}

async function CopyHandler(){
    var value = $("#generated-link").val();
    if (value === "") {
        return;
    }
    navigator.clipboard.writeText(value);
    alert("Ссылка скопирована!");
}

async function GenerateHandler() {
    var value = $("#link-input").val();
    if (value === "") {
        alert("Введите ссылку для сокращения");
        return;
    }

    const response = await fetch("/", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({url: value}),
        error: function(XMLHttpRequest, textStatus){
            alert("Произошла ошибка");
        }
      });

      var res = await response.json();
      $("#generated-link").val(res.url);
}

main();
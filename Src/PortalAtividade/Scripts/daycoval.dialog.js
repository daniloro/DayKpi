function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function ValidarCaractererEspecial(event) {
    var regex = new RegExp("^[@#$&*<>{}]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (regex.test(key)) {
        event.preventDefault();
        return false;
    }
};

var DayImpressao = DayImpressao || (function () {
    var $divImpressao;

    return {
        print: function (div, modo) {
            if (typeof div != "undefined") {
                //div[0].classList.add('w-100');
                $divImpressao = div.html();

            }
            if (modo == 'landscape') {
                $("#printFrame").attr("src", "/Template/ImpressaoPadraoLandscape.aspx");
            } else {
                $("#printFrame").attr("src", "/Template/ImpressaoPadrao.aspx");
            }

        },
        setDiv: function (div) {
            $divImpressao = div.html();
        },
        getDiv: function () {
            return $divImpressao;
        },
        printIframe: function () {
            var browser = navigator.userAgent.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];

            if (browser[1] === "Chrome") {
                $("#printFrame").show();
            }

            var iFrame = $('#printFrame')[0];
            var statusPrint = iFrame.contentWindow.document.execCommand("print", false, null);
            if (statusPrint === false) {
                iFrame.contentWindow.print();
            } else if (browser[1] === "Chrome") {
                $("#printFrame").hide();
            }
        }
    };
}());

var DayMensagens = DayMensagens || (function () {
    var $mensagem = "";

    return {
        mostraMensagemComLink: function (titulo, mensagem, linkBotao, textoBotao) {
            if ($('#dayMensagens').length > 0) {
                $('#dayMensagensLabel').text(titulo);
                $('#dayMensagensBody').html('<p>' + mensagem + '</p>');
                $('#dayMensagens').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);

            } else {
                var $dialog = $(
                    '<div class="modal fade" id="dayMensagens" tabindex="-1" role="dialog" aria-labelledby="dayMensagensLabel" style=" overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" role="document">' +
                    '<div class="modal-content">' +

                    '<div class="modal-header">' +
                    '<span class="line"></span>' +
                    '<h5 class="modal-title" id="dayMensagensLabel">' + titulo + '</h5>' +
                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                    '  <span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>' +

                    '<div class="modal-body" id="dayMensagensBody">' +
                    '<p class="d-flex justify-content-center">' + mensagem + '</p>' +
                    '</div>' +

                    '<div class="modal-footer">' +
                    '<a id="btnMensagemConfirmacao" href="#" class="bbtn btn-outline-primary btn-mobile">' + textoBotao + '</a>' +
                    '</div>' +
                    '</div ></div ></div > ');

                //$('#dayMensagens').remove();
                $('#page-content').append($dialog);
                $("#btnMensagemConfirmacao").attr('href', linkBotao);
                $('#dayMensagens').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);
            }
        },
        mostraMensagem: function (titulo, mensagem) {

            if ($('#dayMensagens').length > 0) {
                $('#dayMensagensLabel').text(titulo);
                $('#dayMensagensBody').html('<p>' + mensagem + '</p>');
                $('#dayMensagens').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);
            } else {
                var $dialog = $(
                    '<div class="modal fade" id="dayMensagens" tabindex="-1" role="dialog" aria-labelledby="dayMensagensLabel" style="overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" role="document">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<span class="line"></span>' +
                    '<h5 class="modal-title" id="dayMensagensLabel">' + titulo + '</h5>' +
                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                    '    <span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>' +
                    '<div class="modal-body" id="dayMensagensBody">' +
                    '<p>' + mensagem + '</p>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<a href="#" class="btn btn-outline-primary btn-mobile" data-dismiss="modal" aria-label="Close">Fechar</a>' +
                    '</div></div></div></div>');

                //$('#dayMensagens').remove();
                $('#page-content').append($dialog);
                $('#dayMensagens').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);
            }
        },
        mostraMensagemLogin: function (titulo, mensagem) {
            $("#dayMensagens").remove();

            var $dialog = $(
                '<div class="modal fade" id="dayMensagens" tabindex="-1" role="dialog" aria-labelledby="dayMensagensLabel" aria-hidden="true">' +
                ' <div class="modal-dialog  modal-dialog-centered" role="document">' +
                ' <div class="modal-content">' +
                '  <div class="modal-header">' +
                '    <span class="line"></span>' +
                '     <h5 class="modal-title" id="dayMensagensLabel">' + titulo + '</h5>' +
                '      <button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                '       <span aria-hidden="true">&times;</span>' +
                '      </button>' +
                '     </div>' +
                '   <div class="modal-body">' +
                '       <i class="material-icons d-flex justify-content-center">error_outline</i>' +
                '       <p class="d-flex justify-content-center">' + mensagem + '</p>' +
                '   </div>' +
                '<div class="modal-footer text-right">' +
                '<button class="btn btn-outline-primary btn-mobile" data-dismiss="modal" aria-label="Close">Fechar</button>' +
                ' </div>' +
                ' </div>' +
                '</div>' +
                '</div > ');

            $('#page-content').append($dialog);
            $('#dayMensagens').modal('show');

            setTimeout(function () {
                DayLoader.hide();
            }, 100);
        },
        mostraMensagemComBotao: function (titulo, mensagem, botao) {
            if ($('#dayMensagensConfirmacao').length > 0) {
                $('#dayMensagensConfirmacaoLabel').text(titulo);
                $('#dayMensagensConfirmacaoBody').html('<p>' + mensagem + '</p>');
                $('#dayMensagensConfirmacao').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);
            } else {
                var $dialog = $(
                    '<div class="modal fade" id="dayMensagensConfirmacao" tabindex="-1" role="dialog" aria-labelledby="dayMensagensConfirmacaoLabel" style=" overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" role="document">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<span class="line"></span>' +
                    '<h5 class="modal-title" id="dayMensagensLabel">' + titulo + '</h5>' +
                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                    '<span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>' +

                    '<div class="modal-body" id="dayMensagensConfirmacaoBody">' +
                    '<p class="d-flex justify-content-center">' + mensagem + '</p>' +
                    '</div>' +


                    '<div class="modal-footer">' +
                    '<a id="btnMensagemConfirmacao" href="#" class="btn btn-outline-primary btn-mobile">OK</a>' +
                    '</div></div></div></div>');

                $('#page-content').append($dialog);
                $("#btnMensagemConfirmacao").attr('href', botao.href === undefined ? botao.attr('href') : botao.href);
                $('#dayMensagensConfirmacao').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);
            }
        },
        mostraMensagemConfirmacao: function (titulo, mensagem, botao) {
            if ($('#dayMensagensConfirmacao').length > 0) {
                $('#dayMensagensConfirmacaoLabel').text(titulo);
                $('#dayMensagensConfirmacaoBody').html('<p>' + mensagem + '</p>');
                $("#btnConfirma").attr('href', botao.href === undefined ? botao.attr('href') : botao.href);
                $('#dayMensagensConfirmacao').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);
            } else {
                var $dialog = $(
                    '<div class="modal fade" id="dayMensagensConfirmacao" tabindex="-1" role="dialog" aria-labelledby="dayMensagensConfirmacaoLabel" style=" overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" role="document">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<span class="line"></span>' +
                    '     <h5 class="modal-title" id="dayMensagensConfirmacaoLabel">' + titulo + '</h5>' +
                    '      <button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                    '       <span aria-hidden="true">&times;</span>' +
                    '      </button>' +
                    '</div>' +
                    '<div class="modal-body" id="dayMensagensConfirmacaoBody">' +
                    '<p >' + mensagem + '</p>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<a id="btnCancela" href="#" class="btn btn-outline-primary btn-mobile" data-dismiss="modal">Cancelar</a>' +
                    '<a id="btnConfirma" href="#" class="btn btn-outline-primary btn-mobile">Confirmar</a>' +
                    '</div></div></div></div>');

                $('#page-content').append($dialog);
                $("#btnConfirma").attr('href', botao.href === undefined ? botao.attr('href') : botao.href);
                $('#dayMensagensConfirmacao').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);

                $(".modal-footer > a").click(function () {
                    var $botao = $("[id$='hfValue'");

                    if ($botao !== null && $botao !== undefined) {
                        var value = $(this).attr("id") === "btnCancela" ? "n" : "s";
                        $botao.val(value);
                    }
                });
            }
        },
        mostraMensagemConfirmacaoPersonalizado: function (titulo, mensagem, mensagemAdicional, botao, textoBotaoCancelar, textoBotaoConfirmar, maxWidth) {
            if ($('#dayMensagensConfirmacaoPersonalizado').length > 0) {
                $('#dayMensagensConfirmacaoPersonalizadoLabel').text(titulo);
                $('#dayMensagensConfirmacaoPersonalizadoBody').html('<p>' + mensagem + '</p><br />' + mensagemAdicional);
                $('#dayMensagensConfirmacaoPersonalizado').modal('show');

                setTimeout(function () {
                    DayLoader.hide();
                }, 100);
            } else {
                var btnConfirma = $("[id$='" + botao + "']");
                var cplBotao = (btnConfirma.length > 0 && btnConfirma[0].innerHTML !== undefined && btnConfirma[0].innerHTML.includes("delete")) ||
                    (botao.length > 0 && botao[0].innerHTML !== undefined && botao[0].innerHTML.includes("delete")) ? "Delete" : "";

                if (maxWidth === undefined || maxWidth === "") {
                    maxWidth = "430px";
                }

                var $dialog = $(
                    '<div class="modal fade" id="dayMensagensConfirmacaoPersonalizado" tabindex="-1" role="dialog" aria-labelledby="dayMensagensConfirmacaoPersonalizadoLabel" style=" overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" role="document" style="max-width:' + maxWidth + ';">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<span class="line"></span>' +
                    '<h5 class="modal-title" id="dayMensagensConfirmacaoPersonalizadoLabel">' + titulo + '</h5>' +
                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                    '   <span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>' +
                    '<div class="modal-body" id="dayMensagensConfirmacaoPersonalizadoBody" >' +
                    mensagem +
                    mensagemAdicional +
                    '</div>' +
                    '<div class="modal-footer" style="border: none;">' +

                    '<a id="btnCancela" href="#" class="btn btn-outline-primary btn-mobile" data-dismiss="modal">Cancelar</a>' +
                    '<a id="btnMensagemConfirma' + cplBotao + '" href="#" class="btn btn-primary btn-mobile ml-0 ml-lg-1">Confirmar</a>' +





                    '</div></div></div></div>');

                $('#page-content').append($dialog);

                $('#btnMensagemConfirma' + cplBotao).click(function () {
                    $('#dayMensagensConfirmacaoPersonalizado').modal('hide');
                    DayLoader.show();
                    if (btnConfirma.length > 0) {
                        btnConfirma.click();
                    }
                });

                setTimeout(function () {
                    DayLoader.hide();
                    $('#dayMensagensConfirmacaoPersonalizado').modal({ backdrop: 'static', keyboard: false, show: true });
                }, 100);
            }
        },
        mostraMensagemConfirmacaoSimNao: function (titulo, mensagem, mensagemAdicional, botaoSim, botaoNao) {

            if ($('#dayMensagensConfirmacaoSimNao').length > 0) {
                $('#dayMensagensConfirmacaoSimNaoLabel').text(titulo);
                $('#dayMensagensConfirmacaoSimNaoBody').html('<p>' + mensagem + '</p>');

                setTimeout(function () {
                    DayLoader.hide();
                    $('#dayMensagensConfirmacaoSimNao').modal({ backdrop: 'static', keyboard: false, show: true });
                }, 100);
            } else {


                var $dialog = $(
                    '<div class="modal fade" id="dayMensagensConfirmacaoSimNao" tabindex="-1" role="dialog" aria-labelledby="dayMensagensConfirmacaoSimNaoLabel" style="overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" role="document">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<span class="line"></span>' +
                    '<h5 class="modal-title" id="dayMensagensConfirmacaoSimNaoLabel">' + titulo + '</h5>' +
                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                    '   <span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>' +
                    '<div class="modal-body" id="dayMensagensConfirmacaoSimNaoBody">' +
                    '<p>' + mensagem + '</p>' +
                    mensagemAdicional +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<a id="btnMensagemConfirmacaoSim" href="#" class="btn btn-outline-primary btn-mobile" data-dismiss="modal">Sim</a>' +
                    '<a id="btnMensagemConfirmacaoNao" href="#" class="btn btn-outline-primary btn-mobile" data-dismiss="modal">Não</a>' +
                    '</div></div></div></div>');

                $('#page-content').append($dialog);

                var btnSim = $("[id$='" + botaoSim + "']");
                var btnNao = $("[id$='" + botaoNao + "']");

                $("#btnMensagemConfirmacaoSim").click(function () {
                    btnSim.click();
                });

                $("#btnMensagemConfirmacaoNao").click(function () {
                    btnNao.click();
                });

                setTimeout(function () {
                    DayLoader.hide();
                    $('#dayMensagensConfirmacaoSimNao').modal({ backdrop: 'static', keyboard: false, show: true });
                }, 100);
            }
        },
        adicionarMensagemAviso: function (mensagem) {
            $mensagem += "<p>" + mensagem + "</p>";
        },
        apagarMensagemAviso: function (mensagem) {
            $mensagem = "";
        },
        verificarMensagemAviso: function () {
            if ($mensagem !== "") {
                DayMensagens.mostraMensagem("Verifique a solicitação.", $mensagem);
                $mensagem = "";
                return false;
            } else {
                return true;
            }
        },
        mensagem: function () {
            return $mensagem;
        },
    };
}());

var DayModal = DayModal || (function () {
    var modalSteak = new Array();
    var bindCloseEvent = true;

    return {
        show: function (titulo) {
            if ($('#dayModal').length > 0) {
                $('#dayModalLabel').text(titulo);
                $('#dayModal').modal('show');
            } else {
                var $dialog = $(
                    '<div class="modal fade" id="dayModal" tabindex="-1" role="dialog" aria-labelledby="dayModalLabel" style="overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" role="document">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<span class="line"></span>' +
                    '<h5 class="modal-title" id="dayModalLabel">' + titulo + '</h5>' +
                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                    '<span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>' +
                    '<div class="modal-body" id="dayModalBody" >' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<a id="btnAbreImpressaoModal" href="#" class="btn btn-outline-primary-icon btn-mobile">            <i class="material-icons">print</i>Imprimir</a>' +
                    '<a href="#" class="btn btn-outline-primary btn-mobile" data-dismiss="modal">Fechar</a>' +
                    '</div></div></div></div>');

                $('#page-content').append($dialog);
                $('#dayModalBody').append($('#divConsultaModal'));
                $('#dayModal').modal('show');

                $("#btnAbreImpressaoModal").click(function () {
                    DayImpressao.print($("#divConsultaModal"));
                    return false;
                });
            }
        },
        showWithoutFooter: function (titulo, idModal, div, callbackOnClose) {
            if ($('#dayModal').length > 0) {
                $('#dayModalLabel').text(titulo);
                $('#dayModal').modal('show');
            } else {
                var $dialog = $(
                    '<div class="modal fade" id="' + idModal + '" tabindex="-1" role="dialog" aria-labelledby="dayModalLabel" style="overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" role="document">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<span class="line"></span>' +
                    '<h5 class="modal-title" id="dayModalLabel' + idModal + '">' + titulo + '</h5>' +
                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                    '    <span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>' +
                    '<div class="modal-body" id="dayModalBody' + idModal + '">' +
                    '</div>' +
                    '</div></div></div>');

                $('#page-content').append($dialog);
                $('#dayModalBody' + idModal).append($(div));
                var modalToShow = $('#' + idModal).modal('show');

                if (callbackOnClose !== undefined && callbackOnClose !== "") {
                    modalToShow.on("hide.bs.modal", function () {
                        callbackOnClose();
                    });
                }

                $("#btnAbreImpressaoModal").click(function () {
                    DayImpressao.print($("#divConsultaModal"));
                    return false;
                });
            }
        },
        getModal: function (titulo, div, modalBodyBackGroundColor, maxWidth, divPai) {
            if (modalBodyBackGroundColor === undefined) {
                modalBodyBackGroundColor = "#fff";
            }
            if (!divPai) {

                divPai = "#page-content";
            }
            if (maxWidth === undefined) {
                maxWidth = "modal-lg"
            }
            /**    
             *     Tamanhos para o modal 
            modal-sm =  Small Modal
			modal-lg = Large Modal
			modal-xl = Full Width Modal
             **/
            var $dialog = $(
                '<div class="modal fade" id="dayModal' + modalSteak.length + '" tabindex="-1" role="dialog" aria-labelledby="dayModalLabel" style="overflow-y:visible">' +
                '<div class="modal-dialog ' + maxWidth + ' modal-dialog-centered" role="document">' +
                '<div class="modal-content" >' +
                '<div class="modal-header">' +
                '    <span class="line"></span>' +
                '     <h5 class="modal-title" id="dayModalLabel' + modalSteak.length + '">' + titulo + '</h5>' +
                '      <button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                '       <span aria-hidden="true">&times;</span>' +
                '      </button>' +
                '</div>' +
                '<div class="modal-body" id="dayModalBody' + modalSteak.length + '" style="background-color:' + modalBodyBackGroundColor + '" >' +
                '</div>' +
                '</div></div></div>');

            $(divPai).append($dialog);
            $("#dayModalBody" + modalSteak.length).append($(div));

            return $("#dayModal" + modalSteak.length).modal('hide');
        },
        addModal: function (titulo, div, modalBodyBackGroundColor, callbackOnClose, maxWidth, divPai) {
            var modalToShow = DayModal.getModal(titulo, div, modalBodyBackGroundColor, maxWidth, divPai);
            if (callbackOnClose !== undefined && callbackOnClose !== "") {
                bindCloseEvent = false;
                modalToShow.on("hide.bs.modal", function () {
                    callbackOnClose();
                });
            }

            if (modalSteak.length > 0 && bindCloseEvent) {
                var modalAnterior = $(modalSteak).get(modalSteak.length - 1);
                modalAnterior.on("hide.bs.modal", function () {
                    modalToShow.modal('show');
                });
            }

            modalSteak.push(modalToShow);
        },
        showModals: function () {
            if (modalSteak.length > 0) {
                var modalToShow = modalSteak.shift();
                modalToShow.modal('show');
            }
        }
    };
}());

var DayLoader = DayLoader || (function () {
    return {
        show: function () {

            if ($('#modalLoader').length > 0) {
                $('#modalLoader').modal('show');
            } else {
                var $dialog = $(
                    '<div class="modal" id="modalLoader" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true"  style="overflow-y:visible">' +
                    '<div class="modal-dialog  modal-dialog-centered" style="max-width:250px">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<h4 style="margin:0;">Carregando...</h4>' +
                    '</div>' +
                    '<div class="modal-body">' +
                    '<div class="progress progress-striped active" style="margin-bottom:0; z-index: 9001;">' +
                    '<div class="progress-bar" style="width: 100%;"></div>' +
                    '</div></div></div></div></div>');

                $('#page-content').append($dialog);
                $('#modalLoader').modal('show');
            }
        },
        hide: function () {
            if ($('#modalLoader').length > 0) {
                $('#modalLoader').modal('hide');
            }
        }
    };
}());
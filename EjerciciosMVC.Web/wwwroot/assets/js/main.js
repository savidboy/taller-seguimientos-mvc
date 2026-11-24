/**
* Template Name: NiceAdmin
* Template URL: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/
* Updated: Mar 17 2024 with Bootstrap v5.3.3
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/

(function () {
  "use strict";

  const select = (el, all = false) => {
    el = el.trim();
    return all ? [...document.querySelectorAll(el)] : document.querySelector(el);
  };

  // Mostrar u ocultar el menu en pantallas pequenas
  const botonMenu = select('.toggle-sidebar-btn');
  if (botonMenu) {
    botonMenu.addEventListener('click', () => {
      select('body').classList.toggle('toggle-sidebar');
    });
  }

  // Sombra del encabezado y boton para volver arriba
  const encabezado = select('#header');
  const volverArriba = select('.back-to-top');

  const alDesplazar = () => {
    const desplazado = window.scrollY > 100;

    if (encabezado) {
      encabezado.classList.toggle('header-scrolled', desplazado);
    }

    if (volverArriba) {
      volverArriba.classList.toggle('active', desplazado);
    }
  };

  window.addEventListener('load', alDesplazar);
  document.addEventListener('scroll', alDesplazar);
})();

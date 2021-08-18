(function (window) {
  window.__env = window.__env || {};
  (window.__env.identityUrl = "http://127.0.0.1:5004"),
    (window.__env.apiGatewayUrl = "http://127.0.0.1:5004"),
    (window.__env.modo = "normal"), // "avancado",
    (window.__env.client = {
      id: "SISGP.Web",
      secret: "secret",
      scope: "SISGP.ProgramaGestao",
    });
})(this);

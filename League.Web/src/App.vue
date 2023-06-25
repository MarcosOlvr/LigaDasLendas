<script setup>
</script>

<template>
  <header>
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark m-2">
        <div class="container">
          <a href="/" class="navbar-brand text-info">Liga das Lendas</a>
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
            <div class="navbar-nav">
              <a href="/" class="nav-item nav-link">Início</a>
              <a href="/champions" class="nav-item nav-link">Campeões</a>
              <a href="/runes" class="nav-item nav-link">Runas</a>
              <a href="/spells" class="nav-item nav-link">Feitiços de invocador</a>
            </div>
          </div>
        </div>
      </nav>
  </header>
<hr>

  <main class="container-fluid">
    <router-view></router-view>
  </main>
</template>
<template>
    <ae-dynamic-window :page="page" :section="field.name" :title="field.caption" :viewModelClassName="field.assemblyQualifiedName" @setBreadcrumb="setBreadcrumb" @teller="listener"></ae-dynamic-window>
</template>

<script>
    /* ******************************************************************
    *
    * window
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {

        },
 
    }

</script>
<style scoped>
</style>
